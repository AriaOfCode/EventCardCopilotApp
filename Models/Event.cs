using System.ComponentModel.DataAnnotations;
using System;
using EventCardCopilotApp.Models.Validation;


namespace EventCardCopilotApp.Models
{
    


public class Event { 

    public int Id { get; set; }
    [Required(ErrorMessage = "Event start date is required.")]
    [FutureDate(ErrorMessage = "The date must be in the future.")]
    public DateTime EventStart   { get; set; }
    public DateTime BookingWindowEnd { get; set; }
    [Required(ErrorMessage = "Name Event is required.")]
    public string EventName { get; set; }= "";
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }= "";
    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; }     = "";           
    public string Download_url { get; set; } = "";
    [Required(ErrorMessage = "Price is required.")]
    [Range(5, 5000, ErrorMessage = "     price must be between 5 and 5000.")]
    public double Price { get; set; }
    
    public List<int> SignedUpUserIds { get; set; } = new();
    public int MaxMembership { get; set; } =100;
    public int Membership => SignedUpUserIds.Count;

    

}
}