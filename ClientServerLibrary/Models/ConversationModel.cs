using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientServerLibrary.DbClasses
{
    [Serializable]
    public enum ConversationAccessibility
    {
        Public,
        Private
    }

    [Serializable]
    public class ConversationModel
    {
        public int Id
        { 
            get => id;
            set => id = value; 
        }
        private int id;

        public string Name
        {
            get => name;
            set => name = value;
        }
        private string name;

        public string Description
        {
            get => description;
            set => description = value;
        }
        private string description;

        public string Avatar
        {
            get => avatar;
            set => avatar = value;
        }
        private string avatar;

        private string streamingPort;
        public string StreamingPort
        {
            get => streamingPort;
            set => streamingPort = value;
        }

        public int CreatorID 
        { 
            get => creatorID;
            set => creatorID = value; 
        }
        private int creatorID;
    }
}