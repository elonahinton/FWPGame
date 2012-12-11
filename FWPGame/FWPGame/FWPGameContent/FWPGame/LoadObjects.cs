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
using System.Collections;


namespace FWPGame
{
    public partial class FWPGame : Microsoft.Xna.Framework.Game
    {
        protected internal Tree motherTree;
        protected internal House motherHouse;
        protected internal List<Sprite> transObj = new List<Sprite>();
        private SproutTree sproutTree;

        //
        protected void LoadObjects()
        {  
            
            // Create a Tree instance
            Texture2D[] burnTreeSequence = {
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
            motherTree = new Tree(Content.Load<Texture2D>("tree/tree"), new Vector2(0, 0), new Vector2(0, 0),
                burnTreeSequence, Content.Load<Texture2D>("tree/burntTree"), multiplyTree);




            // Create a House instance
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
            motherHouse = new House(Content.Load<Texture2D>("house/house"), new Vector2(0, 0), new Vector2(0, 0),
                burnHouseSequence, Content.Load<Texture2D>("house/houseBurnt"));

            sproutTree = new SproutTree(Content.Load<Texture2D>("UI/treeicon"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(sproutTree);
            powers.Add(new Fire(Content.Load<Texture2D>("UI/fireicon"), this, new Vector2(0, 0), new Vector2(0, 0)));
            powers.Add(new BuildHouse(Content.Load<Texture2D>("UI/home"), this, new Vector2(0, 0), new Vector2(0, 0)));

        }

        //public ArrayList SproutTreeTextures()
        //{
        //    ArrayList trees = new ArrayList();
        //    trees.Add(Content.Load<Texture2D>("tree/tree"));
        //    return trees;
        //}

        private void Transmorg()
        {
            Random decision = new Random();
            MapTile[,] mapTiles = map.MapTiles;
            int x;
            int y;

            if (decision.NextDouble() > 0.001)
            {
                return;
            }

            for (int i = 0; i < mapTiles.GetLength(0); ++i)
            {
                if (decision.NextDouble() > 0.45)
                {
                    continue;
                }
                for (int j = 0; j < mapTiles.GetLength(1); ++j)
                {
                    if (decision.NextDouble() > 0.45)
                    {
                        continue;
                    }
                    for (int s = 0; s < mapTiles[i, j].mySprites.Count; ++s)
                    {
                        Sprite newSprite = mapTiles[i, j].mySprites[s].Spread();
                        if (newSprite != null)
                        {
                            if (decision.NextDouble() > 0.5)
                            {
                                x = i + (int)Math.Round(decision.NextDouble());
                                y = j + (int)Math.Round(decision.NextDouble());
                            }
                            else if (decision.NextDouble() > 0.5)
                            {
                                x = i - (int)Math.Round(decision.NextDouble());
                                y = j - (int)Math.Round(decision.NextDouble());
                            }
                            else if (decision.NextDouble() > 0.5)
                            {
                                x = i + (int)Math.Round(decision.NextDouble());
                                y = j - (int)Math.Round(decision.NextDouble());
                            }
                            else 
                            {
                                x = i - (int)Math.Round(decision.NextDouble());
                                y = j + (int)Math.Round(decision.NextDouble());
                            }
                            
                            
                            map.SpreadTile(ref x, ref y);
                            if (mapTiles[x, y].mySprites.Count == 0  ||
                                mapTiles[i, j].mySprites[s].name.Equals(mapTiles[x, y].mySprites[0].name))
                            {
                                mapTiles[x, y].Add(newSprite);
                            }
                            
                            
                        }
                    }

                }
            }
        }

    }

}
