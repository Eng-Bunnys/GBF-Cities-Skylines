using ICities;
using UnityEngine;

namespace HighDemandResMod
{

    public class HighDemandMod : IUserMod
    {

        public string Name
        {
            get { return "High Demand"; }
        }

        public string Description
        {
            get { return "Increases residential demand and the rate of upgrading | Made by Bunnys"; }
        }
    }

    public class HigherResDemand : DemandExtensionBase
    {
        public override int OnCalculateResidentialDemand(int originalDemand)
        {
            int updatedDemand = originalDemand * 2;
            if (updatedDemand < 2) updatedDemand = 20;
            if (updatedDemand > 100) updatedDemand = 100;
            return updatedDemand;
        }

        public override int OnCalculateCommercialDemand(int originalDemand)
        {
            return originalDemand - 20;
        }

        public override int OnCalculateWorkplaceDemand(int originalDemand)
        {
            return originalDemand - 20;
        }
    }

    public class LevelUp : LevelUpExtensionBase
    {
        public override ResidentialLevelUp OnCalculateResidentialLevelUp(ResidentialLevelUp levelUp, int averageEducation, int landValue, ushort buildingID, Service service, SubService subService, Level currentLevel)
        {
            if (levelUp.landValueProgress != 0)
            {
                Level targetLevel;

                if (landValue < 15)
                {
                    targetLevel = Level.Level1;
                    levelUp.landValueProgress = 1 + (landValue * 15 + 7) * 2;
                }
                else if (landValue < 35)
                {
                    targetLevel = Level.Level2;
                    levelUp.landValueProgress = 1 + ((landValue - 15) * 15 + 10) * 2;
                }
                else if (landValue < 60)
                {
                    targetLevel = Level.Level3;
                    levelUp.landValueProgress = 1 + ((landValue - 35) * 15 + 12) * 2;
                }
                else if (landValue < 85)
                {
                    targetLevel = Level.Level4;
                    levelUp.landValueProgress = 1 + ((landValue - 60) * 15 + 12) * 2;
                }
                else
                {
                    targetLevel = Level.Level5;
                    levelUp.landValueProgress = 1;
                }

                if (targetLevel < currentLevel)
                {
                    levelUp.landValueProgress = 1;
                }
                else if (targetLevel > currentLevel)
                {
                    levelUp.landValueProgress = 15;
                }

                if (targetLevel < levelUp.targetLevel)
                {
                    levelUp.targetLevel = targetLevel;
                }
            }

            levelUp.landValueTooLow = false;
            if (currentLevel == Level.Level2)
            {
                if (landValue == 0) levelUp.landValueTooLow = true;
            }
            else if (currentLevel == Level.Level3)
            {
                if (landValue < 2) levelUp.landValueTooLow = true;
            }
            else if (currentLevel == Level.Level4)
            {
                if (landValue < 2) levelUp.landValueTooLow = true;
            }
            else if (currentLevel == Level.Level5)
            {
                if (landValue < 2) levelUp.landValueTooLow = true;
            }

            return levelUp;
        }
    }

}
