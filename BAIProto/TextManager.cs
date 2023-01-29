using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BAIProto
{
    internal class TextManager
    {
        public Vector2 textPosition { get; set; }
        public string textContent { get; set; }
        public SpriteFont font { get; set; }

        public Color spriteColor;
        public TextManager()
        {

        }
        public TextManager(SpriteFont f, string text, Vector2 pos, Color col)
        {
            spriteColor = col;
            this.textPosition = pos;
            this.textContent = text;
            this.font = f;
        }
        public void DrawText(SpriteBatch s)
        {
            s.Begin();

            s.DrawString(font, textContent, textPosition, spriteColor);

            s.End();
        }
    }
}