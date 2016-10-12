﻿namespace AutoCadet.Models
{
    public class InstructorGridItemBaseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
    }
}