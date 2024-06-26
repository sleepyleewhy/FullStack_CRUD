﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WY5JZF_HFT_2023241.Models
{
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DivisionId { get; set; }


        [StringLength(120)]
        public string DivisionName { get; set; }


        [Required]
        public int Population { get; set; }


        public virtual ICollection<Team> Teams { get; set; }
        public Division()
        {
            Teams = new HashSet<Team>();
        }

    }
}
