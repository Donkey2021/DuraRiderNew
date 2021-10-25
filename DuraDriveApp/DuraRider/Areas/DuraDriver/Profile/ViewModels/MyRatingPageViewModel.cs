using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DuraRider.Areas.DuraDriver.Profile.ViewModels
{
    public class MyRatingPageViewModel : AppBaseViewModel
    {
        public class RatingModel
        {
            public string Images { get; set; }
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Date { get; set; }
        }
        public ObservableCollection<RatingModel> ListOfMyRating { get; set; } = new ObservableCollection<RatingModel>()
        {
            new RatingModel() { Images="profile",Id="#73497475", Title="John Doe", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            new RatingModel() { Images="profile",Id="#73497475", Title="Juhi Nakwal", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},            
            new RatingModel() { Images="profile",Id="#73497475", Title="John Doe", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            new RatingModel() { Images="profile",Id="#73497475", Title="Juhi Nakwal", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},          
            new RatingModel() { Images="profile",Id="#73497475", Title="John Doe", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            new RatingModel() { Images="profile",Id="#73497475", Title="Juhi Nakwal", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
           new RatingModel() { Images="profile",Id="#73497475", Title="John Doe", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            new RatingModel() { Images="profile",Id="#73497475", Title="Juhi Nakwal", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
           new RatingModel() { Images="profile",Id="#73497475", Title="John Doe", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            new RatingModel() { Images="profile",Id="#73497475", Title="Juhi Nakwal", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.",Date="23/03/2021"},
            };
    }
}
