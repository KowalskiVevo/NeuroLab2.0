using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace _DGraphics
{
    public class Coord
    {
        public float x , y , z;
        public float phi , theta , radius;

        Coord ()
        {
            x = y = z = phi = theta = 0;
            radius = 1;
        }        

        Coord ( float p1 , float p2 , float p3, bool IsNormal)
        {
            if ( IsNormal )
            {
                x = p1;
                y = p2;
                z = p3;
                phi = 0;
                theta = 3.14f;
                radius = 1;
            }
            else
            {
                phi = p1;
                theta = p2;
                radius = p3;
                x = y = z = 0;
            }
        }

        Coord ( float [] normal , bool empty )
        {
            x = normal [0];
            y = normal [1];
            z = normal [2];
            phi = theta = 0;
            radius = 1;
        }

        Coord ( float [] polar )
        {
            phi = polar [0];
            theta = polar [1];
            radius = polar [2];
            x = y = z = 0;
        }
        float [] ToNormal ()
        {
            float [] normal = new float [4];
            normal [1] = y = radius * (float)Math.Cos(theta);
            normal [2] = z = radius * (float)Math.Sin(phi) * ( float )Math.Sin(theta);
            normal [0] = x = radius * (float)Math.Sin(theta) * ( float )Math.Cos(phi);
            
            return normal;
        }

        float [] ToPolar ()
        {
            float [] polar = new float [4];
            polar [2] = radius = ( float )Math.Sqrt(x * x + y * y + z * z);
            polar [1] = theta = 1 / ( float )Math.Atan(y / Math.Sqrt(x * x + z * z));

            if ( x > 0 || z >= 0 )
                polar [0] = phi = ( float )Math.Atan(z / x);
            else
                if ( x <= 0 )
                    polar [0] = phi = ( float )Math.PI + ( float )Math.Atan(z / x);
                else
                    if ( x >= 0 || z < 0 )
                        polar [0] = phi = ( float )Math.PI * 2 + ( float )Math.Atan(z / x);

            polar [3] = 1;

            return polar;
        }

        public static void RotateNormalCoordinates (ref Vector3 Coords, float phi , float theta )
        {
            Coord tempTranslate = new Coord(Coords.X, Coords.Y, Coords.Z, true);
            tempTranslate.ToPolar();
            tempTranslate.phi = phi;
            tempTranslate.theta = theta;
            tempTranslate.ToNormal();
            
            Coords = new Vector3(tempTranslate.x , tempTranslate.y , tempTranslate.z);
        }

        public static void RotateX (ref float X ,ref float Y , float Angle)
        {
            float Radius = ( float )Math.Sqrt(X * X + Y * Y);
            
            X = Radius * ( float )Math.Cos(Angle);
            Y = Radius * ( float )Math.Sin(Angle);
        }

    }
}
