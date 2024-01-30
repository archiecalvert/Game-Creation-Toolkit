using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

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
        float scale;
        JArray[] layers;
        string spriteSheetLocation;
        string mapDataLocation;
        public Map(JObject json)
        {
            isValid = true;
            //used to make sure the program doesnt crash if there is an error loading in the map data
            try
            {
                //Gets the map data from the json object
                spriteSheetLocation = (string)json["spriteSheetLocation"];
                mapDataLocation = (string)json["mapDataLocation"];
                scale = (float)json["tileScale"];
                originalData = File.ReadAllText(mapDataLocation);
                //Converts the map data into a json object
                jsonData = JsonConvert.DeserializeObject(originalData);
                //Extracts the data from the new map data json object
                mapDimensions.X = jsonData["width"];
                mapDimensions.Y = jsonData["height"];
                tileDimensions.X = jsonData["tilewidth"];
                tileDimensions.Y = jsonData["tileheight"];
                layerCount = jsonData.SelectToken("layers").Count;
                //Gets the spritesheet for the map and loads it into a texture
                TextureAtlas = Texture2D.FromFile(Core._graphics.GraphicsDevice, spriteSheetLocation);
                //Stores the location of each tile in the texture atlas in a list
                for (int y = 0; y < TextureAtlas.Height / tileDimensions.Y; y++)
                {
                    for (int x = 0; x < TextureAtlas.Width / tileDimensions.X; x++)
                    {
                        TileRects.Add(new Rectangle((int)(x * tileDimensions.X), (int)(y * tileDimensions.Y), (int)tileDimensions.X, (int)tileDimensions.Y));
                    }
                }
                //Stores each of the layer data in their own array
                //This allows for tiles to be placed on top of eachother which improves the map
                layers = new JArray[layerCount];
                for (int i = 0; i < layerCount; i++)
                {
                    layers[i] = (JArray)jsonData.SelectToken("layers")[i].SelectToken("data");
                }
                for (int layer = 0; layer < layerCount; layer++)
                {
                    data.Add(layers[layer].ToObject<int[]>());
                }
            }
            catch { isValid = false; }
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            //If there was an error in loading the map data, nothing should be drawn to the screen
            if (!isValid) return;
            int index = 0;
            //iterates through each layer
            for (int l = 0; l < layerCount; l++)
            {
                //iterates through each row
                for (int y = 0; y < mapDimensions.Y; y++)
                {

                    //iterates through each column
                    for (int x = 0; x < mapDimensions.X; x++)
                    {
                        //checks to see if the current tile is blank or not
                        if ((int)data[l][index] != 0)
                        {
                            //calculates the screen position for the tile
                            Vector2 position = new Vector2(x * tileDimensions.X * scale, y * tileDimensions.Y * scale) + new Vector2(MainEditor.GameView.WindowBounds.X, MainEditor.GameView.WindowBounds.Y);
                            Rectangle tile = new Rectangle((int)(position.X),
                                (int)(position.Y),
                                (int)(tileDimensions.X * scale),
                                (int)(tileDimensions.Y * scale));
                            //checks to see whether the tile is in view
                            Rectangle currentRect = TileRects[(int)data[l][index] - 1];
                            float distLeft = position.X - MainEditor.GameView.WindowBounds.Left;
                            //distance between the right of the game view and the texture
                            float distRight = MainEditor.GameView.WindowBounds.Right - position.X - (tileDimensions.X * scale);
                            //distance between the top of the game view and the texture
                            float distTop = position.Y - MainEditor.GameView.WindowBounds.Top;
                            //distance between the bottom of the game view and the texture
                            float distBottom = MainEditor.GameView.WindowBounds.Bottom - position.Y - (tileDimensions.Y * scale);
                            //true when the texture is outside of the left side of the game view
                            if (distLeft < 0)
                            {
                                //extracts the section of the texture thats in view
                                currentRect.Width -= (int)Math.Floor(-distLeft / scale);
                                currentRect.X -= (int)(distLeft / scale);
                                position.X -= distLeft;
                            }
                            //true when the texture is outside of the right side of the game view
                            if (distRight < 0)
                            {
                                //extracts the section of the texture thats in view
                                currentRect.Width -= (int)Math.Floor(-distRight / scale);
                            }
                            //true when the texture is outside of the top side of the game view
                            if (distTop < 0)
                            {
                                //extracts the section of the texture thats in view
                                currentRect.Height -= (int)Math.Floor(-distTop / scale);
                                currentRect.Y -= (int)(distTop / scale);
                                position.Y -= distTop;
                            }
                            //true when the texture is outside of the bottom side of the game view
                            if (distBottom < 0)
                            {
                                //extracts the section of the texture thats in view
                                currentRect.Height -= (int)Math.Floor(-distBottom / scale);
                            }
                            if (isInView(tile))
                            {
                                //Draws the current tile to the screen
                                Core._spriteBatch.Draw(texture: TextureAtlas,
                                                position: new Vector2(position.X, position.Y),
                                                currentRect,
                                                color: Color.White,
                                                rotation: 0f,
                                                Vector2.Zero,
                                                scale: Vector2.One * scale,
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
            return;
        }
    }
}
