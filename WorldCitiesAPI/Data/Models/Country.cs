﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCitiesAPI.Data.Models
{
    [Table("Countries")]
    [Index(nameof(Name))]
    [Index(nameof(ISO2))]
    [Index(nameof(ISO3))]
    public class Country
    {
        /// <summary>
        /// The unique id and primary key for this Country
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Country name (in UTF8 format)
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-2 format)
        /// </summary>
        public string ISO2 { get; set; } = null!;

        /// <summary>
        /// Country code (in ISO 3166-1 ALPHA-3 format)
        /// </summary>
        public string ISO3 { get; set; } = null!;

        /// <summary>
        /// A collection of all cities related to this country.
        /// </summary>
        public ICollection<City>? Cities { get; set; } = null!;
    }
}
