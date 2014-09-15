﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public enum Category
    {
        None = 0,
        
        Resource = 100,
        Resource_Acceleration = 101,
        Resource_Smoothing = 102,
        
        Mustering = 110,
        Mustering_Ally = 110,
        Mustering_Attachment = 111,
        Mustering_Event = 112,
        
        Draw = 120,
        Draw_From_Deck = 121,
        Draw_From_Discard = 122,
        Draw_From_Search_5 = 123,
        Draw_From_Search_10 = 124,
        Draw_From_Search_All = 125,
        
        Discard = 130,
        Discard_From_Hand = 131,
        Discard_From_Play = 132,
        Discard_From_Deck = 133,

        Questing = 140,
        Questing_Boost = 141,
        Question_Action_Advantage = 142,

        Offensive = 150,
        Offensive_Boost = 151,
        Offensive_Action_Advantage = 152,

        Defensive = 160,
        Defensive_Boost = 161,
        Defensive_Action_Advantage = 162,

        Scrying = 170,
        Scrying_Encounter_Deck = 171,
        Scrying_Player_Deck = 172,
        Scrying_Shadow_Cards = 173,
        
        Readying = 180,

        Cancellation = 190,
        Cancellation_Treachery = 191,
        Cancellation_Shadow = 192,
        Cancellation_Damage = 193,

        Recursion = 200,
        Recursion_Hero = 201,
        Recursion_Ally = 202,
        Recursion_Attachment = 203,
        Recursion_Event = 204,

        Healing = 210,
        Healing_Hero = 211,
        Healing_Character = 212,

        Threat = 220,
        Threat_Reduction = 221,
        Threat_Reset = 222,
        Threat_Cancellation = 223,

        Control = 230,
        Control_Questing = 231,
        Control_Travel = 232,
        Control_Engagement = 233,
        Control_Staging_Threat = 234,
        Control_Combat = 235,
        Control_Direct_Damage = 236,
        Control_Encounter_Deck = 237,
        Control_Enemy_Any = 241,
        Control_Enemy_Staging = 242,
        Control_Enemy_Engaged = 243,
        Control_Enemy_Not_Engaged = 244,
        Control_Location_Any = 251,
        Control_Location_Active = 252,
        Control_Location_Staging = 253,
        Control_Treachery = 261,

        Bonus = 270,
        Bonus_Sphere = 271,
        Bonus_Trait = 272,
        Bonus_Keyword = 273,
        Bonus_Willpower = 274,
        Bonus_Attack = 275,
        Bonus_Defense = 276,
        Bonus_Hit_Points = 277,
        
        Scope = 300,
        Scope_Global = 301,
        Scope_Self = 302,
        Scope_Player = 303,
        Scope_All_Players = 304,
        Scope_Character = 311,
        Scope_Hero = 312,
        Scope_Ally = 313,
        Scope_Attachment = 314,
        Scope_Event = 315,
        Scope_Quest = 321,
        Scope_Encounter = 331,
        Scope_Encounter_Staging = 332,
        Scope_Location = 341,
        Scope_Location_Active = 342,
        Scope_Location_Staging = 343,
        Scope_Enemy = 351,
        Scope_Enemy_Staging = 352,
        Scope_Enemy_Engaged = 353,
        Scope_Enemy_Trait = 354,

        Action = 400,
        Action_Any_Phase = 401,
        Action_Resource = 402,
        Action_Planning = 403,
        Action_Quest = 404,
        Action_Travel = 405,
        Action_Encounter = 406,
        Action_Combat = 407,
        Action_Refresh = 408,

        Response = 500,
        Response_Commit = 501,
        Response_Quest_Success = 502,
        Response_Quest_Fail = 503,
        Response_Character_Defend = 511,
        Response_Character_Damaged = 512,
        Response_Character_Healed = 513,
        Response_Character_Attack = 514,
        Response_Character_Deals_Damage = 515,
        Response_Character_Leaves_Play = 516,
        Response_Enemy_Attack = 316,
        Response_Enemy_Destroyed = 517,
        Response_Hero_Destroyed = 518,
        Response_Ally_Destroyed = 519,
        Response_Play_From_Hand = 521,
        Response_Enters_Play = 522,
        Response_Leaves_Play = 523,
        Response_Attach = 524,
        Response_Control = 525,
        Response_Travel = 531,
        Response_Explore = 532,
        Response_Encounter_Revealed = 541,
        Response_Enemy_Revaled = 542,
        Response_Treachery_Revealed = 543,
        Response_Location_Revealed = 544,
        Response_Enemy_Staged = 545,
        Response_Shadow_Dealt = 551,
        Response_Shadow_Trigger = 552,
        Response_Engage = 561,
        Response_Threat_Raise = 571,
        Response_Encounter_Threat_Raise = 572,
        Response_Exhaust = 581,
        Response_Ready = 582,

        Passive = 700,
        Passive_Damage_Tokens = 701,
        Passive_While_Committed = 702,
        Passive_While_Enemy_Revealed = 703,

        Limit_Once_Per_Game = 801,
        Limit_Once_Per_Round = 811,
        Limit_Once_Per_Phase = 821,
        Limit_Twice_Per_Phase = 822,
        Limit_Three_Times_Per_Phase = 823,
        Limit_Title = 831,
        Limit_Sphere = 832,
        Limit_Trait = 833,
        Limit_Keyword = 834
    }
}