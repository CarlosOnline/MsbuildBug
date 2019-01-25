using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Vlc.DotNet.Forms.TypeEditors
{
    public sealed class DirectoryEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select Vlc libraries folder.";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (value != null)
                folderBrowserDialog.SelectedPath = value.ToString();
            folderBrowserDialog.ShowNewFolderButton = true;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return new DirectoryInfo(folderBrowserDialog.SelectedPath);
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
