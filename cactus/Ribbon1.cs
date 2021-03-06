﻿using Microsoft.Office.Tools.Ribbon;

namespace cactus
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            splitButton1.Label = "提取\n内容";
            button1.Label = "导出\n批注";
            button2.Label = "套用\n格式";
            button3.Label = "查找\n内容";
            //button7.Label = "制作\n汇编目录\n";
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            CommentFinder comment = new CommentFinder();
            comment.GetContent();

        }


     
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            Formatter format = new Formatter();
            format.doFormat();
        }

        private void splitButton1_Click(object sender, RibbonControlEventArgs e)
        {
            OrgFinder of = new OrgFinder();
            of.GetContent();
        }

        private void button6_Click(object sender, RibbonControlEventArgs e)
        {
            splitButton1_Click(sender, e);
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            TitleFinder t1 = new TitleFinder(1);
            t1.GetContent();
        }

        private void button5_Click(object sender, RibbonControlEventArgs e)
        {
            TitleFinder t2 = new TitleFinder(2);
            t2.GetContent2();
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

    }
}
