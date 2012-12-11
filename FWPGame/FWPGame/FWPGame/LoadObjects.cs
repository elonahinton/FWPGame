//Camco
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;
using FWPGame.Powers;
using FWPGame.Items;
using System.Collections;


namespace FWPGame
{
    public partial class FWPGame : Microsoft.Xna.Framework.Game
    {
        protected internal Grass myGrass;
        protected internal Road myRoad;
        protected internal Water motherWater;
        protected internal Wood motherWood;
        protected internal Tree motherTree;
        protected internal House motherHouse;
        protected internal Tornado motherTornado;
        protected internal Halo motherHalo;
        protected internal People person;
        protected internal List<Sprite> transObj = new List<Sprite>();
        private SproutTree sproutTree;
        private MakePerson makePerson;
        
        private SoundEffect fireEffect;
        private SoundEffectInstance firePlay;
        private SoundEffect rainEffect;
        private SoundEffectInstance rainPlay;

        //
        protected void LoadObjects()
        {

            // Fire and Rain sound effects
            fireEffect = Content.Load<SoundEffect>("sound/fire");
            firePlay = fireEffect.CreateInstance();
            rainEffect = Content.Load<SoundEffect>("sound/rain");
            rainPlay = rainEffect.CreateInstance();
            

            /* Create Animation sequences */

            // Rain
            Texture2D[] rainingWater = {
                Content.Load<Texture2D>("raining/rain_0"),
                Content.Load<Texture2D>("raining/rain_1"),
                Content.Load<Texture2D>("raining/rain_2"),
                Content.Load<Texture2D>("raining/rain_3"),
                Content.Load<Texture2D>("raining/rain_4"),
                Content.Load<Texture2D>("raining/rain_5"),
                Content.Load<Texture2D>("raining/rain_6"),
                Content.Load<Texture2D>("raining/rain_7"),
                Content.Load<Texture2D>("raining/rain_8"),
                Content.Load<Texture2D>("raining/rain_9"),
                Content.Load<Texture2D>("raining/rain_10"),
                Content.Load<Texture2D>("raining/rain_11"),
                Content.Load<Texture2D>("raining/rain_12"),
                Content.Load<Texture2D>("raining/rain_13"),
                Content.Load<Texture2D>("raining/rain_14"),
                Content.Load<Texture2D>("raining/rain_15"),
                Content.Load<Texture2D>("raining/rain_16"),
                Content.Load<Texture2D>("raining/rain_17"),
                Content.Load<Texture2D>("raining/rain_18"),
                Content.Load<Texture2D>("raining/rain_19"),
                Content.Load<Texture2D>("raining/rain_20"),
                Content.Load<Texture2D>("raining/rain_21")
            };
            
 
            // Burning
            Texture2D[] burningSequence = {
                Content.Load<Texture2D>("burning/burn_0"),
                Content.Load<Texture2D>("burning/burn_1"),
                Content.Load<Texture2D>("burning/burn_2"),
                Content.Load<Texture2D>("burning/burn_3"),
                Content.Load<Texture2D>("burning/burn_4"),
                Content.Load<Texture2D>("burning/burn_5"),
                Content.Load<Texture2D>("burning/burn_6"),
                Content.Load<Texture2D>("burning/burn_7"),
                Content.Load<Texture2D>("burning/burn_8"),
                Content.Load<Texture2D>("burning/burn_9")
            };

            // Squirrel planting
            Texture2D[] multiplyTree = {
                Content.Load<Texture2D>("tree/squirrelplant/planttree_0"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_1"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_2"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_3"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_4"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_5"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_6"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_7"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_8"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_9"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_10"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_11"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_12"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_13"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_14"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_15"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_16"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_17"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_18"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_19"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_20"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_21"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_22"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_23"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_24"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_25"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_26"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_27"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_28"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_29"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_30"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_31"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_32"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_33"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_34"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_35"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_36"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_37"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_38"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_39"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_40"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_41"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_42"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_43"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_44"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_45"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_46"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_47"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_48"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_49"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_50"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_51"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_52"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_53"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_54"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_55"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_56"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_57"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_58"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_59"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_60"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_61"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_62"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_63"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_64"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_65"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_66"),
                Content.Load<Texture2D>("tree/squirrelplant/planttree_67")
            };

            // Wood cutting
            Texture2D[] fellTree = {
                Content.Load<Texture2D>("wood/fell/fell_0"),
                Content.Load<Texture2D>("wood/fell/fell_1"),
                Content.Load<Texture2D>("wood/fell/fell_2"),
                Content.Load<Texture2D>("wood/fell/fell_3"),
                Content.Load<Texture2D>("wood/fell/fell_4")
            };

            // House burning
            Texture2D[] burnHouseSequence = {
                Content.Load<Texture2D>("house/houseFire"),
                Content.Load<Texture2D>("burning/burn_0"),
                Content.Load<Texture2D>("burning/burn_1"),
                Content.Load<Texture2D>("burning/burn_2"),
                Content.Load<Texture2D>("burning/burn_3"),
                Content.Load<Texture2D>("burning/burn_4"),
                Content.Load<Texture2D>("burning/burn_5"),
                Content.Load<Texture2D>("burning/burn_6"),
                Content.Load<Texture2D>("burning/burn_7"),
                Content.Load<Texture2D>("burning/burn_8"),
                Content.Load<Texture2D>("burning/burn_9")
            };

            /* Create Mother instances of items */

            motherWater = new Water(Content.Load<Texture2D>("water"), new Vector2(0, 0), new Vector2(0, 0),
                rainingWater, rainPlay);

            myGrass = new Grass(Content.Load<Texture2D>("grass"), new Vector2(0, 0), new Vector2(0, 0),
                burningSequence, Content.Load<Texture2D>("burntGrass"), firePlay);

            myRoad = new Road(Content.Load<Texture2D>("infrastructure/dirtRoad"),
                new Vector2(0, 0), new Vector2(0, 0), burningSequence,
                Content.Load<Texture2D>("infrastructure/burntTown"), firePlay,
                Content.Load<Texture2D>("infrastructure/highway"),
                Content.Load<Texture2D>("infrastructure/village"),
                Content.Load<Texture2D>("infrastructure/city"));

            motherHouse = new House(Content.Load<Texture2D>("house/house"), new Vector2(0, 0), new Vector2(0, 0),
                burnHouseSequence, Content.Load<Texture2D>("house/houseBurnt"), firePlay, 
                Content.Load<Texture2D>("house/houseLit"), myRoad);

            motherWood = new Wood(Content.Load<Texture2D>("wood/logs"), new Vector2(0, 0), new Vector2(0, 0),
                fellTree, burningSequence, Content.Load<Texture2D>("wood/logsBurnt"), firePlay, motherHouse);

            motherTree = new Tree(Content.Load<Texture2D>("tree/tree"), new Vector2(0, 0), new Vector2(0, 0),
                burningSequence, Content.Load<Texture2D>("tree/burntTree"), firePlay,
                Content.Load<Texture2D>("tree/burntTree"), multiplyTree, motherWood);

            myRoad = new Road(Content.Load<Texture2D>("infrastructure/dirtRoad"),
                new Vector2(0, 0), new Vector2(0, 0), burningSequence,
                Content.Load<Texture2D>("infrastructure/burntTown"), firePlay,
                Content.Load<Texture2D>("infrastructure/highway"),
                Content.Load<Texture2D>("infrastructure/village"),
                Content.Load<Texture2D>("infrastructure/city"));

            person = new People(Content.Load<Texture2D>("people/person"), new Vector2(0, 0), new Vector2(0, 0), null,
                Content.Load<Texture2D>("people/humanBurn"), Content.Load<Texture2D>("people/electrocute/electrocute_7"));


            Texture2D[] spinTornadoSequence = {
                Content.Load<Texture2D>("Tornado/tor_00"),
                Content.Load<Texture2D>("Tornado/tor_01"),
                Content.Load<Texture2D>("Tornado/tor_02"),
                Content.Load<Texture2D>("Tornado/tor_03"),
                Content.Load<Texture2D>("Tornado/tor_04"),
                Content.Load<Texture2D>("Tornado/tor_05"),
                Content.Load<Texture2D>("Tornado/tor_06"),
                Content.Load<Texture2D>("Tornado/tor_07"),
                Content.Load<Texture2D>("Tornado/tor_08"),
                Content.Load<Texture2D>("Tornado/tor_09"),
                Content.Load<Texture2D>("Tornado/tor_10"),
                Content.Load<Texture2D>("Tornado/tor_11"),
                Content.Load<Texture2D>("Tornado/tor_12")};

            motherTornado = new Tornado(spinTornadoSequence, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0));
            motherHalo = new Halo(Content.Load<Texture2D>("Protect/bubble"), new Vector2(0, 0), new Vector2(0, 0));
        }


        /* The Transmorg routine applies random transformations to the entire world.
         * Some of the transformation relationships are meant to appear as "Acts of gods"
         * while others are the apparent effect of human activity.
         * 
         * Most of the logic to effect the transformations is in the objects themselves.
         * This framework of code is a non-AI randomizer that traverses the entire map
         * of the world, selecting items to "transmorg".
         * 
         * *Bad code* choices:
         * The "magic numbers" are just that - flexible tuning numbers for random decisions.
         * Liberal use of "continue/return" while never necessary, due to the desire to most
         * often do nothing, arguably makes this particular routine easier to understand.
         */ 
        private void Transmorg()
        {
            Random decision = new Random();
            MapTile[,] mapTiles = map.MapTiles;
            int x;
            int y;

            // Most of the time, don't do anything at all..
            if (decision.NextDouble() > 0.001)
            {
                return;
            }

            for (int i = 0; i < mapTiles.GetLength(0); ++i)
            {
                if (decision.NextDouble() < 0.65)
                {
                    continue;
                }
                for (int j = 0; j < mapTiles.GetLength(1); ++j)
                {
                    //  Randomize the frequency of spreading
                    if (decision.NextDouble() > 0.45)
                    {
                        continue;
                    }
                    for (int s = 0; s < mapTiles[i, j].mySprites.Count; ++s)
                    {
                        //  Randomize the selected tiles on the map
                        if (decision.NextDouble() > 0.3)
                        {
                            continue;
                        }
                        Sprite newSprite = mapTiles[i, j].mySprites[s].Spread();
                        if (newSprite != null)
                        {
                            // Randomly choose an adjacent tile
                            if (decision.NextDouble() < 0.5)
                            {
                                x = i + (int)Math.Round(decision.NextDouble());
                                y = j + (int)Math.Round(decision.NextDouble());
                            }
                            else if (decision.NextDouble() > 0.5)
                            {
                                x = i - (int)Math.Round(decision.NextDouble());
                                y = j - (int)Math.Round(decision.NextDouble());
                            }
                            else if (decision.NextDouble() < 0.5)
                            {
                                x = i + (int)Math.Round(decision.NextDouble());
                                y = j - (int)Math.Round(decision.NextDouble());
                            }
                            else 
                            {
                                x = i - (int)Math.Round(decision.NextDouble());
                                y = j + (int)Math.Round(decision.NextDouble());
                            }

                            // Make sure we have a valid tile
                            map.SpreadTile(ref x, ref y);

                            // If this is road, they get upgraded in place in human development
                            if (newSprite.name.Equals("Road"))
                            {
                                mapTiles[i, j].Add(newSprite);
                                //  Don't spread roads, highways, villages too much
                                if (decision.NextDouble() < 0.4)
                                {
                                    continue;
                                }
                            }

                            // Only spread to an empty tile
                            if (mapTiles[x, y].mySprites.Count == 0)
                            {
                                mapTiles[x, y].Add(myGrass.Clone());
                                mapTiles[x, y].Add(newSprite);
                            }


                            // Do random development and/or destruction
                             else if (decision.NextDouble() < 0.4)
                            {
                                int top = mapTiles[x, y].mySprites.Count - 1;
                                Sprite develop = mapTiles[x, y].mySprites[top].Transform();
                                if (develop != null)
                                {
                                    mapTiles[x, y].Clear();
                                    mapTiles[x, y].Add(develop);
                                }
                            }   
                        }
                    }
                }
            }
        }
    }
}
