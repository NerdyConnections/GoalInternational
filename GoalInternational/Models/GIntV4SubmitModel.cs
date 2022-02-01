
namespace GoalInternational.Models
{
    public class GIntV4SubmitModel
    {
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public bool CanUserSubmit { get; set; }
        public bool? SignOff { get; set; }
    }
}