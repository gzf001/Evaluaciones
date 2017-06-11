using Evaluaciones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Models
{
    public class PartialSwal : PartialViewResult
    {
        public PartialSwal(SwalAlert swal)
        {
            if (swal is SwalAlert)
            {
                this.Title = swal.Title;
                this.Message = swal.Message;
                this.eventType = swal.EventType;
            }
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