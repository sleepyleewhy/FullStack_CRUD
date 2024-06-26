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
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [StringLength(240)]
        public string PlayerName { get; set; }

        [Range(1,5)]
        public int Position { get; set; }

        public double AvgPoints { get; set; }

        public int Salary { get; set; }

        [JsonIgnore]
        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamID { get; set; }

        public Player()
        {
            AvgPoints = 0;
            Salary = 0;
        }

    }
}
