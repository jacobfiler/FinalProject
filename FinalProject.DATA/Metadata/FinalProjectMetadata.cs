using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.DATA//.Metadata
{
    #region Application
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }

    public class ApplicationMetadata
    {
        //public int ApplicationID { get; set; }
        [Required(ErrorMessage = "*User ID is required")]
        [StringLength(128, ErrorMessage = "* Maximum 128 Characters")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "*Open Position is required")]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Open Position")]
        public int OpenPositionID { get; set; }

        public System.DateTime ApplicationDate { get; set; }

        [StringLength(200, ErrorMessage = "*Max 200 Characters")]
        public string ManagerNotes { get; set; }

        [Display(Name = "Application Status")]
        [Required(ErrorMessage = "*Application Status is Required")]
        public int ApplicationStatusID { get; set; }

        [StringLength(75, ErrorMessage = "*Max 75 Characters")]
        public string ResumeFilename { get; set; }
    }
    #endregion

    #region ApplicationStatus
    [MetadataType(typeof(ApplicationStatusMetadata))]
    public partial class ApplicationStatus { }

    public class ApplicationStatusMetadata
    {
        //public int ApplicationStatusID { get; set; }
        [Required(ErrorMessage = "Status Name is Required")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "Status Name")]
        public string Statusname { get; set; }

        [Required(ErrorMessage = "Status Description is Required")]
        [StringLength(250, ErrorMessage = "*Max 250 Characters")]
        [Display(Name = "Status Description")]
        public string StatusDescription { get; set; }
    }
    #endregion

    #region Location
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }

    public class LocationMetadata
    {
        //public int LocationID { get; set; }

        [Required(ErrorMessage = "*Store Number is Required")]
        [StringLength(10, ErrorMessage = "*Max 10 Characters")]
        [Display(Name = "Store Number")]
        public string StoreNumber { get; set; }

        [Required(ErrorMessage = "*City is Required")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "*State is Required")]
        [StringLength(2, ErrorMessage = "*Max 2 Characters")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "*Manager is Required")]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Manager")]
        public string ManagerID { get; set; }
    }
    #endregion

    #region OpenPosition
    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition { }

    public class OpenPositionMetadata
    {
        //public int OpenPositionID { get; set; }
        [Required(ErrorMessage = "*Location is Required")]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "*Open Position is Required")]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Open Position")]
        public int PositionID { get; set; }
    }
    #endregion


    #region Position
    [MetadataType(typeof(PositionMetadata))]
    public partial class Position { }

    public class PositionMetadata
    {
        //public int PositionID { get; set; }
        [Required(ErrorMessage = "*Title is Required")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "Position")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*Job Description is Required")]
        [StringLength(1073741791, ErrorMessage = "*Description Too Long")]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
    }
    #endregion

    #region UserDetails
    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetails { }

    public class UserDetailsMetadata
    {
        //public string UserID { get; set; }
        [Required(ErrorMessage = "*First Name is Required")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is Required")]
        [StringLength(50, ErrorMessage = "*Max 50 Characters")]
        [Display(Name = "Position")]
        public string LastName { get; set; }

        
        [StringLength(75, ErrorMessage = "*Max 75 Characters")]
        [Display(Name = "Resume")]
        public string ResumeFilename { get; set; }
    }
    #endregion
}
