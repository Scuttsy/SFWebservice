using System.Collections.Generic;
namespace SFWebservice.Modules
{
    //Player data View Model
    public class PlayerData
    {
        /// Fields as they are represented as tables in the database
        /// <summary cref= https://portal.azure.com/#@hogeschool-wvl.be/resource/subscriptions/628c2816-8b5c-4e10-934c-b401dd558722/resourceGroups/SFRG/providers/Microsoft.Sql/servers/sfdb02/databases/SFDB01/overview>  
        /// Players
        /// Gamesessions
        /// Dodges
        /// Hits
        /// Objectives completed
        /// <summary>

        public int ID
        {
            get
            {
                return Player != null ? Player.PlayerId : -1;
            }
        }

        public Player Player { get; set; }
        public List<GameSession>? Sessions { get; set; } = new List<GameSession>();

        public List<Hit>? Hits { get; set; } = new List<Hit>();
        public List<Dodge>? Dodges { get; set; } = new List<Dodge>();
        public List<ObjectiveCompleted>? ObjectivesCompleted { get; set; } = new List<ObjectiveCompleted>();

    }

}
