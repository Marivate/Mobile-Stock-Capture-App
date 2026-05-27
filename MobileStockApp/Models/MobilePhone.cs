using System;

namespace MobileStockApp.Models
{
    /// <summary>
    /// Model class representing a Mobile Phone record
    /// </summary>
    public class MobilePhone
    {
        /// <summary>
        /// Unique identifier for the mobile phone
        /// </summary>
        public string MobileCode { get; set; }

        /// <summary>
        /// Manufacturer/Make of the mobile phone
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MobilePhone() { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        public MobilePhone(string mobileCode, string make, int quantity)
        {
            MobileCode = mobileCode;
            Make = make;
            Quantity = quantity;
        }

        /// <summary>
        /// Validates the mobile phone object
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(MobileCode) &&
                   !string.IsNullOrWhiteSpace(Make) &&
                   Quantity >= 0;
        }
    }
}
