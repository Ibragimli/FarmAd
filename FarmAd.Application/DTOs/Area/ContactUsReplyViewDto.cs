using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.Area
{
    public class ContactUsReplyViewDto
    {
        public int ContactUsId { get; set; }
        public string Fullname { get; set; }
        public string ContactText { get; set; }
        public ReplyContactPostDto ReplyContactPostDto { get; set; }
    }
    public class ReplyContactPostDto
    {
        public int ContactUsId { get; set; }
        public string Email { get; set; }
        public string ReplyText { get; set; }

    }
}
