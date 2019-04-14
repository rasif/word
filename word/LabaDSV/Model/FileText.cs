using System;
using System.Windows.Media;
using LabaDSV.Helpers;
using LabaDSV.Interface;

namespace LabaDSV.Model
{
    public sealed class FileText:ViewModelBase, IFileText
    {
        #region Constructors

        public FileText()
        {
            Size = 16;
            Text = String.Empty;
            Font = new FontFamily("Segou UI Light");
        }

        #endregion

        #region Properties

        public int Size
        {
            get { return _size; }
            set
            {
                if (value > 10 && value < 40)
                    UpdateValue(value, ref _size);
            }
        }

        public string Text
        {
            get { return _text; }
            set { UpdateValue(value, ref _text);}
        }

        public FontFamily Font
        {
            get { return _font; }
            set { UpdateValue(value, ref _font); }
        }
  
        #endregion

        #region Fields

        private int _size;
        private string _text;
        private FontFamily _font;

        #endregion

    }
}
