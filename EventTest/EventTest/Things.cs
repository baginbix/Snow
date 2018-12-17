using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    class Things
    {
        static Random rand = new Random();
        Texture2D texture;
        public Vector2 position;
        float speed;
        float radius;
        Vector2 velocity;
        float gravity = 0.0001f;
        float xOffset = 0;
        float angle;
        int imageX;
        int imageY;
        

        public Things()
        {
            texture = Game1.tex;
            position = new Vector2(rand.Next(0, 1920), rand.Next(-100,-10));
            radius = GetSize();
            angle = RandomFloat(0, MathHelper.TwoPi);
            imageX = rand.Next(16);
            imageY = rand.Next(16);

        }

        public void Update()
        {
            xOffset = (float)Math.Sin(angle) * radius/100;
            velocity.Y += gravity  * radius;
            velocity.X = xOffset;
            if (velocity.Y > radius * 0.2f)
                velocity.Y = radius * 0.2f;
            position += velocity;
            angle += (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y)/1000;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,new Rectangle(position.ToPoint(),new Point((int)radius,(int)radius)),new Rectangle(imageX*32,imageY*32,32,32), Color.White,angle,new Vector2(16,16), SpriteEffects.None,0);
        }

        public void ApplyForce(Vector2 force)
        {
            velocity += force;
        }

        int GetSize()
        {
            while (true)
            {
                int r1 = rand.Next(1,40);
                int r2 = rand.Next(1,40);
                if (r2 > r1)
                    return r1;
            }
        }
        float RandomFloat(float min, float max)
        {
            return (float)rand.NextDouble() * (max - min) + min;
        }
    }
}
