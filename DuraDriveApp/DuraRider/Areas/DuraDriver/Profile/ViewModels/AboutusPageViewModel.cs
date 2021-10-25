using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuraRider.Areas.DuraDriver.Profile.ViewModels
{
   public class AboutusPageViewModel : AppBaseViewModel
    {
        private string  _title;
        public string Title
        {
            get { return _title; }
            set { _title = value;OnPropertyChanged(nameof(Title)); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value;OnPropertyChanged(nameof(Description)); }
        }

        public AboutusPageViewModel()
        {
            Title = "Lorem ipsum";
            Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
        }

    }
}
