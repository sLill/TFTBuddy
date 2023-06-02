////////////////////////////////////
// Auto-generated using json2csharp
////////////////////////////////////
namespace TFTBuddy.Data
{
    public class ServerStatusData
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> locales { get; set; }
        public List<object> maintenances { get; set; }
        public List<Incident> incidents { get; set; }

        public class Incident
        {
            public object archive_at { get; set; }
            public List<Update> updates { get; set; }
            public object updated_at { get; set; }
            public object maintenance_status { get; set; }
            public DateTime created_at { get; set; }
            public string incident_severity { get; set; }
            public List<Title> titles { get; set; }
            public List<string> platforms { get; set; }
            public int id { get; set; }

            public class Update
            {
                public List<Translation> translations { get; set; }
                public DateTime updated_at { get; set; }
                public List<string> publish_locations { get; set; }
                public DateTime created_at { get; set; }
                public string author { get; set; }
                public int id { get; set; }
                public bool publish { get; set; }

                public class Translation
                {
                    public string locale { get; set; }
                    public string content { get; set; }
                }
            }

            public class Title
            {
                public string locale { get; set; }
                public string content { get; set; }
            }
        }
    }

    public class SummonerData
    {
        public string id { get; set; }
        public string accountId { get; set; }
        public string puuid { get; set; }
        public string name { get; set; }
        public int profileIconId { get; set; }
        public long revisionDate { get; set; }
        public int summonerLevel { get; set; }
    }

    public class ChallengerLeagueData
    {
        public string tier { get; set; }
        public string leagueId { get; set; }
        public string queue { get; set; }
        public string name { get; set; }
        public List<Entry> entries { get; set; }

        public class Entry
        {
            public string summonerId { get; set; }
            public string summonerName { get; set; }
            public int leaguePoints { get; set; }
            public string rank { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public bool veteran { get; set; }
            public bool inactive { get; set; }
            public bool freshBlood { get; set; }
            public bool hotStreak { get; set; }
        }
    }

    public class PlayerMatchesData
    {
        public List<string> MatchIds { get; set; }
    }

    public class MatchData
    {
        public Metadata metadata { get; set; }
        public Info info { get; set; }

        public class Metadata
        {
            public string data_version { get; set; }
            public string match_id { get; set; }
            public List<string> participants { get; set; }
        }

        public class Info
        {
            public long game_datetime { get; set; }
            public double game_length { get; set; }
            public string game_version { get; set; }
            public List<Participant> participants { get; set; }
            public int queue_id { get; set; }
            public string tft_game_type { get; set; }
            public string tft_set_core_name { get; set; }
            public int tft_set_number { get; set; }

            public class Participant
            {
                public List<string> augments { get; set; }
                public Companion companion { get; set; }
                public int gold_left { get; set; }
                public int last_round { get; set; }
                public int level { get; set; }
                public int placement { get; set; }
                public int players_eliminated { get; set; }
                public string puuid { get; set; }
                public double time_eliminated { get; set; }
                public int total_damage_to_players { get; set; }
                public List<Trait> traits { get; set; }
                public List<Unit> units { get; set; }

                public class Companion
                {
                    public string content_ID { get; set; }
                    public int item_ID { get; set; }
                    public int skin_ID { get; set; }
                    public string species { get; set; }
                }

                public class Trait
                {
                    public string name { get; set; }
                    public int num_units { get; set; }
                    public int style { get; set; }
                    public int tier_current { get; set; }
                    public int tier_total { get; set; }
                }

                public class Unit
                {
                    public string character_id { get; set; }
                    public List<string> itemNames { get; set; }
                    public string name { get; set; }
                    public int rarity { get; set; }
                    public int tier { get; set; }
                }
            }
        }
    }
}
