﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WY5JZF_HFT_2023241.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [StringLength(120)]
        public string TeamName { get; set; }


        public virtual ICollection<Player> Players { get; set; }

        [Required]
        public int FanCount { get; set; }

        [JsonIgnore]
        public virtual Division Division { get; set; }

        [ForeignKey("Division")]
        public int DivisionID { get; set; }
        public Team()
        {
            Players = new HashSet<Player>();
        }

    }
}
