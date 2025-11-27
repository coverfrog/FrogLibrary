using UnityEngine;

namespace FrogLibrary
{
    public static class SpriteUtil
    {
        public static Sprite CreateSolidSprite(
            Color color,
            int ppu = 1,
            bool mipmap = false,
            bool makeNoLongerReadable = false)
        {
            // Texture 생성
            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, mipmap);
            tex.SetPixel(0, 0, color);
            tex.Apply(updateMipmaps: mipmap, makeNoLongerReadable: makeNoLongerReadable);

            // Sprite 생성
            return Sprite.Create(
                tex,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f),
                ppu);
        }
    }
}