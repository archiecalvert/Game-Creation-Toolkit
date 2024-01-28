using Assimp.Configs;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpFont.Cache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Map : Script
    {
        Texture2D TextureAtlas;
        string originalData;
        dynamic jsonData;
        Vector2 tileDimensions;
        Vector2 mapDimensions;
        List<Rectangle> TileRects = new List<Rectangle>();
        List<int[]> data = new List<int[]>();
        bool isValid = false;
        int layerCount;
        JArray[] layers;
        public Map(string spriteSheetLocation, string mapDataLocation)
        {
            isValid = true;
            try
            {
                originalData = File.ReadAllText(mapDataLocation);
                //Converts the map data into a json object
                jsonData = JsonConvert.DeserializeObject(originalData);
                //Extracts the data from the new json object
                mapDimensions.X = jsonData["width"];
                mapDimensions.Y = jsonData["height"];
                tileDimensions.X = jsonData["tilewidth"];
                tileDimensions.Y = jsonData["tileheight"];
                layerCount = jsonData.SelectToken("layers").Count;
                //Gets the spritesheet for the map
                TextureAtlas = Texture2D.FromFile(Core._graphics.GraphicsDevice, spriteSheetLocation);
                for (int y = 0; y < TextureAtlas.Height / tileDimensions.Y; y++)
                {
                    for (int x = 0; x < TextureAtlas.Width / tileDimensions.X; x++)
                    {
                        TileRects.Add(new Rectangle((int)(x * tileDimensions.X), (int)(y * tileDimensions.Y), (int)tileDimensions.X, (int)tileDimensions.Y));
                    }
                }
                layers = new JArray[layerCount];
                for (int i = 0; i < layerCount; i++)
                {
                    layers[i] = (JArray)jsonData.SelectToken("layers")[i].SelectToken("data");
                }
                for (int curLay = 0; curLay < layerCount; curLay++)
                {
                    data.Add(layers[curLay].ToObject<int[]>());
                }
            }
            catch { isValid=false; }
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            if(!isValid) return;
            int index = 0;
            for (int l = 0; l < layerCount; l++) {
                for (int y = 0; y < mapDimensions.Y; y++)
                {
                    for (int x = 0; x < mapDimensions.X; x++)
                    {
                        if ((int)data[l][index] != 0)
                        {
                            Vector2 position = new Vector2(x * tileDimensions.X, y * tileDimensions.Y) + new Vector2(MainEditor.GameView.WindowBounds.X, MainEditor.GameView.WindowBounds.Y);

                            Rectangle tile = new Rectangle((int)(position.X),
                                (int)(position.Y),
                                (int)(tileDimensions.X),
                                (int)tileDimensions.Y);



                            if (isInView(tile))
                            {
                                Core._spriteBatch.Draw(texture: TextureAtlas,
                                                position: new Vector2(position.X, position.Y),
                                                TileRects[(int)data[l][index] - 1],
                                                color: Color.White,
                                                rotation: 0f,
                                                Vector2.Zero,
                                                scale: Vector2.One,
                                                SpriteEffects.None,
                                                layerDepth: 0.06f + (0.01f * l));
                            }
                        }
                        index++;
                    }
                    
                }
                index = 0;
            }
        }
        public override void DestroyScript()
        {
            
        }
    }
}
