namespace Reprizo.Areas.Admin.ViewModels.Contact
{
	public class ContactVM
    {
        public string PhoneOne { get; set; }
        public string PhoneTwo { get; set; }
        public string MailOne { get; set; }
        public string MailTwo { get; set; }
        public string WeekDays { get; set; }
        public string Weekends { get; set; }
        public string Address { get; set; }
        public string ContactBanner { get; set; }
		public ContactMessageCreateVM ContactMessage { get; set; }

	}
}
