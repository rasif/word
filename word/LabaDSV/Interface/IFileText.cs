using System.Windows.Media;

namespace LabaDSV.Interface
{
    public interface IFileText
    {
        int Size { get; set; }
        string Text { get; set; }
        FontFamily Font { get; set; }
    }
}
