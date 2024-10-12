using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Pages
{
    public class PatientsAddProfileViewModel
    {
        #region private members
        private ActivityModel selectedActivity;
        private ObjectiveModel selectedObjective;
        private MacronutrientModel selectedMacronutrient;

        private string selectedSex;

        #endregion

        #region Properties
        public ActivityModel SelectedActivity
        {
            get { return this.selectedActivity; }
            set
            {
                if (this.selectedActivity != value)
                {
                    this.selectedActivity = value;
                }
            }
        }
        public ObjectiveModel SelectedObjective
        {
            get { return this.selectedObjective; }
            set
            {
                if (this.selectedObjective != value)
                {
                    this.selectedObjective = value;
                }
            }
        }
        public MacronutrientModel SelectedMacronutrient
        {
            get { return this.selectedMacronutrient; }
            set
            {
                if (this.selectedMacronutrient != value)
                {
                    this.selectedMacronutrient = value;
                }
            }

        }
        public string SelectedSex
        {
            get { return this.selectedSex; }
            set
            {
                if (this.selectedSex != value)
                {
                    this.selectedSex = value;
                }
            }
        }
        #endregion


    }
}
