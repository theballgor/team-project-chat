using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ClientServerLibrary.DbClasses
{
    [Serializable]
    public enum FileType
    {
        Text,
        Audio,
        File,
        Image
    }
    [Table("DbFiles")]
    [Serializable]
    public class DbFile : INotifyPropertyChanged
    {
        public int Id { get; set; }
        [StringLength(4000)]
        public string FileName { get { return fileName; } set { fileName = value; OnPropertyChanged("FileName"); } }
        [NotMapped]
        private string fileName;
        [Required]
        public string FilePath { get { return filePath; } set { filePath = value; OnPropertyChanged("FilePath"); } }
        [NotMapped]
        private string filePath;
        public FileType FileType { get { return fileType; } set { fileType = value; OnPropertyChanged("FileType"); } }
        [NotMapped]
        private FileType fileType;
        [Column("message_id")]
        public virtual Message Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}