﻿using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cactus
{
    class TitleFinder : AFinder
    {
        // 查找一级标题并提取
        private int type;
        private Regex reLevel1;
        private Regex reLevel2;

        public TitleFinder(int find_type)
        {
            type = find_type;
            reLevel1 = new Regex("^[一二三四五六七八九十]+、");
            reLevel2 = new Regex("^（[一二三四五六七八九十]+）");
        }

        private List<String> __parse_file()
        {
            StreamReader sr = new StreamReader(tmp_file, Encoding.Default);
            String line;
            List<String> draft_list = new List<String>();

            Regex reg;
            if (type == 1)
            {
                reg = reLevel1;
            }
            else
            {
                reg = reLevel2;
            }

            while ((line = sr.ReadLine()) != null)
            {
                Match match = reg.Match(line.TrimStart());
                if (match.Success)
                {
                    String b = match.Groups[0].ToString();
                    draft_list.Add(line);
                    //System.Windows.Forms.MessageBox.Show("bb" + line.Trim() + "\n");

                }
                //Debug.WriteLine(match.ToString(), line.ToString());
            }
            sr.Close();
            return draft_list;
        }

        private List<String> __parse_title()
        {
            List<String> title_list = new List<String>();

            Document thisDoc = Globals.ThisAddIn.Application.ActiveDocument;
            Paragraphs pars = thisDoc.Paragraphs;
            foreach (Paragraph item in pars)
            {
                if (item.OutlineLevel == WdOutlineLevel.wdOutlineLevel2)
                {
                    title_list.Add(item.Range.Text);
                }
            }
            return title_list;
        }

        private void __print_to_file(List<String> final_list)
        {
            Document newDoc = null;
            // Create An New Word   
            newDoc = Globals.ThisAddIn.Application.Documents.Add();
            newDoc.Content.Paragraphs[1].Range.Font.Size = 16;
            newDoc.Content.Paragraphs[1].Range.Font.Name = "方正仿宋_GBK";
            newDoc.Content.Paragraphs[1].Range.Font.NameAscii = "Times New Roman";
            Paragraph par = newDoc.Content.Paragraphs.Add();
            par.OutlineLevel = WdOutlineLevel.wdOutlineLevel1;

            par.Range.Text = "来自文档：“" + src_file + "”中的一级标题共" + final_list.Count + "项。";
            par.Range.InsertParagraphAfter();

            par.Range.InsertAfter("--------------------------------分割线-------------------------------");
            par.Range.InsertParagraphAfter();

            par.Range.InsertAfter(" ");
            par.Range.InsertParagraphAfter();

            foreach (String item in final_list)
            {
                par.Range.InsertAfter(item);

                par.Range.InsertParagraphAfter();
            }
        }

        public override void GetContent()
        {
            List<String> list = __parse_file();
            __print_to_file(list);
            ClearTmp();
        }
    }
}
