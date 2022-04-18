using DrawPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Pattern
{
    class CPattern : CDrawPattern
    {
        public Point start;
        public Point end;

        public int[,] PATTERN_XX;
        public int similarity;

        public static int FLAG_SIMILARITY; //1 - max //2 - min

        public static int[,] EMPTY;

        public static int[,] getEmptyPattern(int row, int column)
        {
            EMPTY = new int[row,column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    EMPTY[i, j] = 0;
                }
            }
            return EMPTY;
        }

        //--------------------------------------------------------------------------//
        //                                                                          //
        //              CPattern()                                                  //
        //              [Constructor - 1]                                           //
        //                                                                          //
        //              Input parameters:                                           //
        //                      Point s - coordinates start pixel (x,y)             //
        //                      Point e - coordinates end pixel (x,y)               //
        //                      int [,] letter - pattern array                      //
        //                      int row - pattern x size                            //
        //                      int column - pattern y size                         //
        //                                                                          //
        //              Describe:                                                   //
        //                 Create new pattern object.                               //
        //                                                                          //
        //--------------------------------------------------------------------------//
        public CPattern(Point s, Point e, int[,] letter, int row, int column)
        {
            PATTERN_XX = new int[row, column];

            FLAG_SIMILARITY = 1;

            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    PATTERN_XX[i, j] = letter[i, j];

            start.X = s.X; start.Y = s.Y;
            end.X = e.X; end.Y = e.Y;
        }

        //--------------------------------------------------------------------------//
        //                                                                          //
        //              CPattern()                                                  //
        //              [Constructor - 2]                                           //
        //                                                                          //
        //              Input parameters:                                           //
        //                      Point s - coordinates start pixel (x,y)             //
        //                      Point e - coordinates end pixel (x,y)               //
        //                      char letter_number - pattern character              //
        //                      int row - pattern x size                            //
        //                      int column - pattern y size                         //
        //                                                                          //
        //              Describe:                                                   //
        //                 Create new pattern object.                               //
        //                                                                          //
        //--------------------------------------------------------------------------//
        public CPattern(Point s, Point e, char letter_number, int row, int column)
        {
            PATTERN_XX = new int[row, column];

            FLAG_SIMILARITY = 1;


            start.X = s.X; start.Y = s.Y;
            end.X = e.X; end.Y = e.Y;
        }
 
    }
}
