using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;

namespace ClientServerLibrary.DbClasses
{
    [Table("MessageFiles")]
    [Serializable]

    public class MessageFile
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string FileName { get { return fileName; } set { fileName = value; FileExtenction = Path.GetExtension(value); OnPropertyChanged("FileName"); } }
        [NotMapped]
        private string fileName;
        [Required]
        public string FilePath { get { return filePath; } set { filePath = value; fileName = Path.GetFileName(value); OnPropertyChanged("FilePath"); } }
        [NotMapped]
        private string filePath;
        public FileType FileType { get { return fileType; } set { fileType = value; OnPropertyChanged("FileType"); } }
        [NotMapped]
        private FileType fileType;

        public MessageFile() { }
        public MessageFile(string filePath, byte[] fileData)
        {
            FilePath = filePath;
            FileData = fileData;
        }
        [NotMapped]
        public byte[] FileData { get; set; }
        [NotMapped]
        public string FileExtenction { get; set; }
        [Column("message_id")]
        public virtual Message Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool GetFileFromPath()
        {
            try
            {
                if (Directory.Exists(FilePath))
                {
                    FileData = File.ReadAllBytes(FilePath);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SaveFileByPath()
        {
            try
            {
                if (Directory.Exists(FilePath))
                {
                    File.WriteAllBytes(FilePath, FileData);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ImageCheck()
        {
            string[] extentions = ConfigurationManager.AppSettings["ImageExtenctions"].Split(',');
            foreach (var ext in extentions)
            {
                if (FileExtenction == ext)
                    return true;
            }
            return false;
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
