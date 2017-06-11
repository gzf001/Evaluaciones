using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluaciones.Helpers
{
    public class SwalAlert
    {
        public SwalAlert(string message, SwalTypeEvent typeEvent)
        {
            this.Message = message;
            this.EventType = typeEvent;
        }

        public SwalAlert(string title, string message, SwalTypeEvent typeEvent)
        {
            this.Title = title;
            this.Message = message;
            this.EventType = typeEvent;
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            private set
            {
                this.title = value;
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            private set { this.message = value; }
        }

        private SwalTypeEvent eventType;
        public SwalTypeEvent EventType
        {
            get { return eventType; }
            private set { this.eventType = value; }
        }
    }
}