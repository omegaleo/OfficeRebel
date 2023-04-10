using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class MethodExtensions
    {
        /// <summary>
        /// Method to obtain a random element from inside a list
        /// </summary>
        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0) return default;
            
            var r = new System.Random();

            var randomIndex = r.Next(list.Count);

            var returnValue = list[randomIndex];

            return returnValue ?? list[0];

        }
        
        /// <summary>
        /// Convert a Texture2D into a Sprite
        /// </summary>
        public static Sprite ToSprite(this Texture2D texture2D)
        {
            return Sprite.Create(texture2D, new Rect(0,0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        }
        
        /// <summary>
        /// Convert the sprite into a Texture that only has the sprite we want, mostly to be used with Spritesheets
        /// </summary>
        public static Texture2D ToTexture(this Sprite sprite)
        {
            // If the sprite is already the same size as it's base texture, return the texture
            if (sprite.rect.width == sprite.texture.width && sprite.rect.height == sprite.texture.height)
                return sprite.texture;
        
            // Else, we will crop the texture to match the size of the sprite rect, example, a sprite inside a spritesheet
            // Create the new texture
            var croppedTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
            
            // Obtain all the pixels that are inside the sprite rect's area
            var pixels = sprite.texture.GetPixels(  (int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height,  0);
            
            // Set the pixels we obtained into the new texture
            croppedTexture.SetPixels( pixels );

            // Save the changes
            croppedTexture.Apply();

            return croppedTexture;
        }

        public static Sprite ShiftEmployeeColors(this Sprite sprite)
        {
            var colors = new EmployeeColorShift();
            var texture = sprite.ToTexture();
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            
            for (int x = 0; x <= texture.width; x++)
            {
                for (int y = 0; y <= texture.height; y++)
                {
                    if (texture.GetPixel(x, y) == colors.LookingForHair)
                    {
                        texture.SetPixel(x, y, colors.ReplaceWithHair);
                    }
                    else if (texture.GetPixel(x, y) == colors.LookingForClothes)
                    {
                        texture.SetPixel(x, y, colors.ReplaceWithClothes);
                    }
                    else if (texture.GetPixel(x, y) == colors.LookingForClothesShadow)
                    {
                        texture.SetPixel(x, y, colors.ReplaceWithClothesShadow);
                    }
                }
            }
            
            return texture.ToSprite();
        }
    }