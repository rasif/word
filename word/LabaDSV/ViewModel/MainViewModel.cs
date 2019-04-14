using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LabaDSV.Helpers;
using LabaDSV.Interface;
using LabaDSV.Model;
using LabaDSV.View;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace LabaDSV.ViewModel
{
    public sealed class MainViewModel:ViewModelBase
    {
        #region Constructors

        public MainViewModel(ICommandFactory commandFactory, IWindowService windowService)
        {
            if (commandFactory == null)
                return;

            if (windowService == null)
                return;

            _commandFactory = commandFactory;
            _windowService = windowService;

            ConfigureCommands();

            FileInstance = new FileText();

            _dictionary = new Hashtable();
            _nameList = new List<string>();
            _textErrorList = new List<string>();

            _validateTextTimer = new DispatcherTimer();
            _validateTextTimer.Tick += ValidateText;
            _validateTextTimer.Interval = new TimeSpan(0,0,0,5);

            LoadWordsFromFile("Dictionary.txt");
        }


        #endregion

        #region Commands

        private void ConfigureCommands()
        {
            MoveWindowCommand = _commandFactory.CreateCommand<Window>(OnMoveWindowCommand);
            CloseWindowCommand = _commandFactory.CreateCommand<Window>(OnCloseWindowCommand);
            CollapseWindowCommand = _commandFactory.CreateCommand<Window>(OnCollapseWindowCommand);
            SaveFileCommand = _commandFactory.CreateCommand(OnSaveFileCommand);
            OpenFileCommand = _commandFactory.CreateCommand(OnOpenFileCommand);
            AddFileCommand = _commandFactory.CreateCommand(OnAddFileCommand);
            FindLongestWordCommand = _commandFactory.CreateCommand(OnFindLongestWordCommand);
            FindShortestWordCommand = _commandFactory.CreateCommand(OnFindShortestWordCommand);
            FindCommonWordCommand = _commandFactory.CreateCommand(OnFindCommonWordCommand);
            FindRarelyWordCommand = _commandFactory.CreateCommand(OnFindRarelyWordCommand);
            FindWordTenCommand = _commandFactory.CreateCommand(OnFindWordTenCommand);
            IncreaseFontSizeCommand = _commandFactory.CreateCommand(OnIncreaseFontSizeCommand);
            DecreaseFontSizeCommand = _commandFactory.CreateCommand(OnDecreaseFontSizeCommand);
            ChangeColorCommand = _commandFactory.CreateCommand(OnChangeColorCommand);
            SearchWordCommand = _commandFactory.CreateCommand<TextBox>(OnSearchWordCommand);
            FindLastWordCommand = _commandFactory.CreateCommand(OnFindLastWordCommand);
            ShowChartCommand = _commandFactory.CreateCommand(OnShowChartCommand);
            TextChangedCommand = _commandFactory.CreateCommand<ListBox>(OnTextChangedCommand);
            SelectionChangedCommand = _commandFactory.CreateCommand<string>(OnSelectionChangedCommand);
            ShowTextErrorCommand = _commandFactory.CreateCommand(OnShowTextErrorCommand);
            StartTextErrorCommand = _commandFactory.CreateCommand(OnStartTextErrorCommand);
            AddWordCommand = _commandFactory.CreateCommand<string>(OnAddWordCommand);
        }

        public ICommand MoveWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand CollapseWindowCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand AddFileCommand { get; set; }
        public ICommand FindLongestWordCommand { get; set; }
        public ICommand FindShortestWordCommand { get; set; }
        public ICommand FindCommonWordCommand { get; set; }
        public ICommand FindRarelyWordCommand { get; set; }
        public ICommand FindWordTenCommand { get; set; }
        public ICommand IncreaseFontSizeCommand { get; set; }
        public ICommand DecreaseFontSizeCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public ICommand SearchWordCommand { get; set; }
        public ICommand FindLastWordCommand { get; set; }
        public ICommand ShowChartCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ShowTextErrorCommand { get; set; }
        public ICommand StartTextErrorCommand { get; set; }
        public ICommand AddWordCommand { get; set; }

        #endregion

        #region Methods for commands

        private void OnMoveWindowCommand(Window window) 
        {
            window.DragMove();
        }

        private void OnCloseWindowCommand(Window window) 
        {
            window.Close();
        }

        private void OnCollapseWindowCommand(Window window) 
        {
            window.WindowState = WindowState.Minimized;
        }

        private async void OnSaveFileCommand() 
        {
            await SaveFile();
        }

        private Task SaveFile() 
        {
            return Task.Run(() =>
            {
                var fileDialog = new OpenFileDialog();

                fileDialog.Filter = "Текстовые файлы |*.txt";

                var isChooseFile = (fileDialog.ShowDialog() == true);
                if (isChooseFile)
                {
                    var nameOfFile = fileDialog.FileName;

                    using (var stream = new FileStream(nameOfFile, FileMode.Create))
                    using (var file = new StreamWriter(stream))
                    {
                        file.Write(FileInstance.Text);
                    }
                }
            });
        }

        private async void OnOpenFileCommand() 
        {
            await OpenFile();
        }

        private Task OpenFile() 
        {
            return Task.Run(() =>
            {
                var fileDialog = new OpenFileDialog();

                fileDialog.Filter = "Текстовые файлы |*.txt";

                var isChooseFile = (fileDialog.ShowDialog() == true);
                if (isChooseFile)
                {
                    var nameOfFile = fileDialog.FileName;

                    using (var stream = new FileStream(nameOfFile, FileMode.Open))
                    using (var file = new StreamReader(stream))
                    {
                        FileInstance.Text = file.ReadToEnd();
                    }
                }
            });
        }

        private async void OnAddFileCommand() 
        {
            await AddFile();
        }

        private Task AddFile() 
        {
            return Task.Run(() =>
            {
                var fileDialog = new OpenFileDialog();

                fileDialog.Filter = "Текстовые файлы |*.txt";

                var isChooseFile = (fileDialog.ShowDialog() == true);
                if (isChooseFile)
                {
                    var nameOfFile = fileDialog.FileName;

                    using (var stream = new FileStream(nameOfFile, FileMode.Open))
                    using (var file = new StreamReader(stream))
                    {
                        FileInstance.Text +="\n" + file.ReadToEnd();
                    }
                }
            });
        }   

        private async void OnFindLongestWordCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindLongestWord();

            MessageBox.Show($"Самое длинное слово: {result} ");
        }

        private Task<string> FindLongestWord() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] {' ', '.', ',', '!', '?'},
                    StringSplitOptions.RemoveEmptyEntries);

                var longestWord = words.First(w => w.Length == words.Max(l => l.Length));

                return longestWord;
            });
        }

        private async void OnFindShortestWordCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindShortestWord();

            MessageBox.Show($"Самое короткое слово: {result} ");
        }

        private Task<string> FindShortestWord() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                var shortestWord = words.First(w => w.Length == words.Min(l => l.Length));

                return shortestWord;
            });
        }

        private async void OnFindCommonWordCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindCommondWord();

            MessageBox.Show($"Самое распространенное слово: {result} ");
        }

        private Task<string> FindCommondWord() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                var commonWord = words.GroupBy(s => s)
                    .Where(g => g.Any())
                    .OrderByDescending(g => g.Count())
                    .First().Key;

                return commonWord;
            });
        }

        private async void OnFindRarelyWordCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindRarelyWord();

            MessageBox.Show($"Самое редкое слово: {result} ");
        }

        private Task<string> FindRarelyWord() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                var rarelyWord = words.GroupBy(s => s)
                .Where(g => g.Count() == 1)
                .OrderByDescending(g => g.Count())
                .First().Key;

                return rarelyWord;
            });
        }

        private async void OnFindWordTenCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindWordTen();

            MessageBox.Show($"Слово, которое встречается 10 раз: {result} ");
        }

        private Task<string> FindWordTen() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                var wordTenTimes = words.GroupBy(s => s)
                .Where(g => g.Count() == 5)
                .OrderByDescending(g => g.Count())
                .First().Key;

                return wordTenTimes;
            });
        }

        private async void OnFindLastWordCommand() 
        {
            if (FileInstance.Text == String.Empty)
                return;

            var result = await FindLastWord();

            MessageBox.Show($"Самое последнее слово: {result} ");
        }

        private Task<string> FindLastWord() 
        {
            return Task.Run(() =>
            {
                var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                return words[words.Length - 1];
            });
        }

        private async void OnIncreaseFontSizeCommand() 
        {
            await IncreaseFontSize();
        }

        private Task IncreaseFontSize() 
        {
            return Task.Run(() =>
            {
                FileInstance.Size += 2;
            });
        }

        private async void OnDecreaseFontSizeCommand() 
        {
            await DecreaseFontSize();
        }

        private Task DecreaseFontSize() 
        {
            return Task.Run(() =>
            {
                FileInstance.Size -= 2;
            });
        }

        private void OnChangeColorCommand() 
        {
            var dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var brush = new SolidColorBrush(new Color()
                {
                    A = dialog.Color.A,
                    R = dialog.Color.R,
                    G = dialog.Color.G,
                    B = dialog.Color.B
                });

                Brush = brush;
            }
        }

        private void OnSearchWordCommand(TextBox text) 
        {
            if (SearchText == null)
                return;

            var startIndex = FileInstance.Text.IndexOf(SearchText, StringComparison.Ordinal);

            if (startIndex < 0)
                return;

            text.Select(startIndex, SearchText.Length);
        }

        private async void OnShowChartCommand() 
        {
            if (string.IsNullOrEmpty(FileInstance.Text))
                return;

            await GetFileStatisticsTask();

            _windowService.Show<SimpleWindow>(this);
        }

        private Task GetFileStatisticsTask() 
        {
            return Task.Run(() =>
            {
                Values = GetFileStatistics();
            });
        }

        private List<KeyValuePair<string, int>> GetFileStatistics() 
        {
            var list = new List<KeyValuePair<string, int>>();
            var wordList = new Dictionary<string, int>();

            var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (wordList.ContainsKey(word))
                    ++wordList[word];
                else
                    wordList.Add(word, 1);
            }

            list.AddRange(wordList);

            return list;
        }

        private void OnTextChangedCommand(ListBox listBox) 
        {
            var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var autoList = new List<string>();

            if (words.Length!= 0)
            {
                var typedString = words[words.Length-1];

                if (!string.IsNullOrEmpty(typedString))
                {
                    Parallel.For(0, _nameList.Count, (i) =>
                    {
                        if (_nameList[i].StartsWith(typedString))
                            autoList.Add(_nameList[i]);
                    });
                }
            }

            if (autoList.Count > 0)
            {
                listBox.ItemsSource = autoList;
                listBox.Visibility = Visibility.Visible;
            }
            else if (FileInstance.Text.Equals(""))
            {
                listBox.Visibility = Visibility.Collapsed;
                listBox.ItemsSource = null;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
                listBox.ItemsSource = null;
            }
        }

        private void OnSelectionChangedCommand(string word) 
        {
            FileInstance.Text += word;
        }

        private void OnShowTextErrorCommand() 
        {
            _windowService.Show<TextErrorView>(this);
        }

        private void OnStartTextErrorCommand() 
        {
            if (_isTimerStart == false)
            {
                _validateTextTimer.Start();
                _isTimerStart = true;
            }
            else
            {
                _validateTextTimer.Stop();
                _isTimerStart = false;
            }
        }

        private void OnAddWordCommand(string word)
        {
            if (!File.Exists("Dictionary.txt"))
                return;

            using (var file = File.AppendText("Dictionary.txt"))
                file.WriteLine(word);

            MessageBox.Show($"Слово {word} добавлено в словарь");
        }

        #endregion

        #region Other methods

        private void LoadWordsFromFile(string name) 
        {    
            if (!File.Exists(name))
                return;

            var word = string.Empty;

            using (var file = new StreamReader(name))
            {
                while (!file.EndOfStream)
                {
                    word = file.ReadLine();

                    if (word != null)
                    {
                        if (!_dictionary.ContainsKey(word))
                        {
                            _dictionary.Add(word, word);
                            _nameList.Add(word);
                        }
                    }
                }
            }
        }

        private void ValidateText(object sender, EventArgs e) 
        {
            _textErrorList.Clear();

            var words = FileInstance.Text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.For(0, words.Length, (i) =>
            {
                if (!_dictionary.ContainsValue(words[i]))
                    _textErrorList.Add(words[i]);
            });
        }

        #endregion

        #region Properties

        public FileText FileInstance { get; set; }

        public string SearchText 
        {
            get { return _searchText; }
            set { UpdateValue(value,ref _searchText);}
        }

        public Brush Brush 
        {
            get { return _brush; }
            set { UpdateValue(value, ref _brush);}
        }

        public List<KeyValuePair<string, int>> Values 
        {
            get { return _values; }
            set { UpdateValue(value, ref _values); }
        }

        public List<string> TextErrorList
        {
            get { return _textErrorList; }
            set { UpdateValue(value, ref _textErrorList);}
        }

        #endregion

        #region Fields

        private readonly ICommandFactory _commandFactory;
        private readonly IWindowService _windowService;

        private Brush _brush;
        private string _searchText;

        private List<KeyValuePair<string, int>> _values;
        private readonly List<string> _nameList;
        private List<string> _textErrorList;
        private readonly Hashtable _dictionary;
        private DispatcherTimer _validateTextTimer;
        private bool _isTimerStart;

        #endregion
    }
}
