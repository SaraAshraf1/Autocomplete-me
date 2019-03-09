using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj_algo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //search the word in dictionery 
        bool Spell_checker(string [] arr, long first, long last, string value)
        {
            long m = (last + first) / 2;
            if (last == first && value.CompareTo(arr[first]) != 0)
                //this mean that we are at the end of list and the word not found       
                return false;

            else if (first > last)
                return false;

            if (last == first && value.CompareTo(arr[first]) == 0)
                //this mean that we are at the end of list but the word was found (at the end of dic)
                return true;

            int comp = string.Compare(arr[m].ToLower(), value);

            if (comp < 0) //the value is greater than the middl
                return Spell_checker(arr, m + 1, last, value);
               
            else if (comp > 0) //the value is less than the middle
                return Spell_checker(arr, first, m - 1, value);

            else //the word is found
                return true;

        }
        //find the first index of the searched list (that should be appeared)
        long binsearchlow(string[] sentence, long first, long last, string value)
        {
            long m=0;
            if (last == 1)
                m = last;
             if (first > last)
                return -1;
            else if (last == first ) //this mean that we are at the end of list
            {
                if (sentence[first].ToLower().StartsWith(value) || value.ToLower().StartsWith(sentence[first])) //check whether the value at end of list starts with the entered value or not
                    return first;
                else
                    return -1; //the value is not found in the list
            }
           else
                    m = (last + first)  / 2;
            int comp=0;
            //compare the value at tha middle to the entered value
            string u = sentence[m];
            if (value.Length < sentence[m].Length)
                comp = string.Compare(sentence[m].Substring(0, sentence[m].Length).ToLower(), value.ToString());
            else
                comp = string.Compare(sentence[m].Substring(0, sentence[m].Length).ToLower(), value.ToString());

            if (sentence[m].ToLower().StartsWith(value) ) //if we found the value
            {
                if (m == 0)
                    m++;  
                if (!sentence[m - 1].ToLower().StartsWith(value)) //this is the first index
                    return m;
                else
                    // continue searshing
                    return binsearchlow(sentence, first, m - 1, value);

            }
            //if we didn't find the value
            else if (comp < 0)//the value is greater than the middle
                return binsearchlow(sentence, m + 1, last, value);

            else//the value is less than the middle
                return binsearchlow(sentence, first, m - 1, value);

        }

        //find the last index of the searched list (that should be appeared)
        // note : if the value is not found in ((binsearchlow)) function ,the ((binsearchupper)) function will not be reached
        long binsearchupper(string[] sentence, long first, long last, string value)
        {
            long m = 0;
            if (first == last - 1 )
                     m = first;
            if (first == last)//this mean that we are at the end of list
                return last;
            else if (first > last)
                return -1;
            else
                m = (first + last) / 2;
            int comp = 0;
            //compare the value at tha middle to the entered value
            if (value.Length < sentence[m].Length)
                comp = string.Compare(sentence[m].Substring(0, value.Length).ToLower(), value.ToString());
            else
                comp = string.Compare(sentence[m].Substring(0, sentence[m].Length).ToLower(), value.ToString());
            
            if (sentence[m].ToLower().StartsWith(value) )
            {
                if (!sentence[m + 1].ToLower().StartsWith(value)) //this is the last index
                    return m;
                else  //continue searshing
                    return binsearchupper(sentence, m + 1, last, value);
            }
            //if we didn't find the value
            else if (comp < 0) //the value is greater than the middle 
                return binsearchupper(sentence, m + 1, last, value);

            else //the value is less than the middle
                return binsearchupper(sentence, first, m - 1, value);
        }

        // get the searched list (that should be appeared)
        Dictionary<string, long> searched_list(long first_index, long last_index, string[] key, long[] val)
        {
            Dictionary<string, long> newdic = new Dictionary<string, long>();
          
            while (first_index <= last_index)
            {
                
                newdic.Add(key[first_index], val[first_index]);
                first_index++;
            }
              return newdic;
        }
        string[] insertionSort(Dictionary<string, long> dic)
        {
            long key = 0;
            string[] sortedArr = dic.Keys.ToArray<string>();
            long[] values = dic.Values.ToArray<long>();
            for (int j = 1; j < dic.Count; j++)       // O(N)*O(N)   
            {                                        //where N = the length of dictionary
                key = values[j]; 
                int i = j - 1;
                while (i > -1 && values[i] < key)     // O(N)
                {
                    //swap key (the string)
                    string tmp = sortedArr[i];
                    sortedArr[i] = sortedArr[i + 1];
                    sortedArr[i + 1] = tmp;

                    //swap value (the weight)
                    long tmp2 = values[i];
                    values[i] = values[i + 1];
                    values[i + 1] = tmp2;
                    i--;
                }
            }
            return sortedArr;
        }

        // //keys
        public static void quickSortInAscendingOrder(long[] numbers, string[] keys, long low, long high)
        {

            long i = low;
            long j = high;
            long temp;
            string temp2;
            string middle = keys[(low + high) / 2];

            while (i < j)
            {
                while (keys[i].CompareTo(middle) < 0)
                {
                    i++;
                }
                while (keys[j].CompareTo(middle) > 0)
                {
                    j--;
                }
                if (j >= i)
                {
                    temp = numbers[i];
                    temp2 = keys[i];
                    numbers[i] = numbers[j];
                    keys[i] = keys[j];
                    numbers[j] = temp;
                    keys[j] = temp2;
                    i++;
                    j--;
                }
            }
            if (low < j)
            {
                quickSortInAscendingOrder(numbers, keys, low, j);
            }
            if (i < high)
            {
                quickSortInAscendingOrder(numbers, keys, i, high);
            }
        }

         
        //values
        public static void quickSortInDescendingOrder(long[] numbers, string[] keys, long low, long high)
        {

            long i = low;
            long j = high;
            long temp;
            string temp2;
            long middle = numbers[(low + high) / 2];

            while (i < j)
            {
                while (numbers[i] > middle)
                {
                    i++;
                }
                while (numbers[j] < middle)
                {
                    j--;
                }
                if (j >= i)
                {
                    temp = numbers[i];
                    temp2 = keys[i];
                    numbers[i] = numbers[j];
                    keys[i] = keys[j];
                    numbers[j] = temp;
                    keys[j] = temp2;
                    i++;
                    j--;
                }
            }


            if (low < j)
            {
                quickSortInDescendingOrder(numbers, keys, low, j);
            }
            if (i < high)
            {
                quickSortInDescendingOrder(numbers, keys, i, high);
            }
        }
        public static int[,] arr;

        // Edit Distance
        int MinNumberOfChanges(string s1, string s2, int s1length, int s2length)
        {
            arr = new int[s1length + 1, s2length + 1];
            arr[0, 0] = 0;

            for (int i = 0; i <= s1length; i++)
            {
                for (int j = 0; j <= s2length; j++)
                {
                    if (i == 0)
                        arr[i, j] = j ;

                    if (j == 0)
                        arr[i, j] = i ;
                }
            }
            for (int i = 1; i <= s1length; i++)
            {
                for (int j = 1; j <= s2length; j++)
                {

                    if (s1[i - 1] == s2[j - 1])
                        arr[i, j] = arr[i - 1, j - 1];
                    else
                    {
                        int R1 = arr[i - 1, j] ; //delete
                        int R2 = arr[i - 1, j - 1] ; //replace 
                        int R3 = arr[i, j - 1] ;  //insert
                        arr[i, j] = Math.Min(R1, Math.Min(R2, R3)) +1 ;
                    }

                }
            }
            return arr[s1length, s2length];
        }
        static Dictionary<string, long> dic = new Dictionary<string, long>();
        static Dictionary<string, long> ndic = new Dictionary<string, long>();
        long[] sorted_dic_weights = dic.Values.ToArray<long>();
        string[] sorted_dic_keys = dic.Keys.ToArray<string>();
        List<string> wordslist =new List <string >();
       int j = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            //open scenario file
            FileStream FS = new FileStream("Search Links (Small).txt", FileMode.Open);
            StreamReader sr = new StreamReader(FS);
            string line;
            line = sr.ReadLine();
            while (sr.Peek() != -1)
            {
                line = sr.ReadLine();
                string[] arr = line.Split(',');
                dic.Add(arr[1], long.Parse(arr[0])); //fill the dic from scenario file
                ndic.Add(arr[1], long.Parse(arr[0]));
            }
            sorted_dic_weights = dic.Values.ToArray<long>();
            sorted_dic_keys = dic.Keys.ToArray<string>();

            quickSortInAscendingOrder(sorted_dic_weights, sorted_dic_keys, 0, dic.Count - 1);
          
            //open dictionary file
            FileStream FS2 = new FileStream("Dictionary (Small).txt", FileMode.Open);
            StreamReader sr2 = new StreamReader(FS2);
            line = sr2.ReadLine();
            while (sr2.Peek() != -1)
            {
                line = sr2.ReadLine();
                wordslist.Add(line);
            }
          string [] words = wordslist.ToArray();     
        }
        string str2 = " "; string str = " "; int c1 = 0;
        List<string> c = new List<string>();
        //string[] c = new string [1];
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           
          
            label4.Visible = false;
           
           // quickSortInAscendingOrder(sorted_dic_weights, sorted_dic_keys, 0, dic.Count - 1);
            
            if (string.IsNullOrWhiteSpace(textBox6.Text.ToString()))
            {
                listBox1.Visible = false;
                sorted_dic_weights = ndic.Values.ToArray<long>();
                sorted_dic_keys = ndic.Keys.ToArray<string>();

                quickSortInAscendingOrder(sorted_dic_weights, sorted_dic_keys, 0, ndic.Count - 1);
                dic = ndic;
                j = 0;
            }
            else
            {

                str2 = textBox6.Text;
                if (str2.Length < str.Length) //delete char
                    dic = ndic;
                int count = dic.Count - 1;
                string content = textBox6.Text.ToLower();
                    //"southern hemispheres "; 
                string[] c3 = content.Split(' ');  // array of strings in content 

                int num_string = c3.Length - 2; //  number of strings in content without the last space

                long indexupper = 0;
                long indexlower = binsearchlow(sorted_dic_keys, 0, sorted_dic_keys.Length-1, content);
                if (indexlower != -1)//if we find the value
                {
                    if(content.Last()==' ')
                    {
                        c.Add(c3[num_string]);
                    }
                    if (indexlower == sorted_dic_keys.Length - 1) //if the first index = length of the array
                        indexupper = sorted_dic_keys.Length - 1;// then the last index will be also = length of the array
                    else
                        indexupper = binsearchupper(sorted_dic_keys, 0, sorted_dic_keys.Length - 1, content);

                    dic = searched_list(indexlower, indexupper, sorted_dic_keys, sorted_dic_weights); //update dic
                    listBox1.Visible = true;

                    if (radioButton1.Checked == true)
                        listBox1.DataSource = insertionSort(dic).ToList<string>();

                    if (radioButton2.Checked == true)
                    {
                        long[] weight_array = dic.Values.ToArray<long>();
                        string[] keys_array = dic.Keys.ToArray<string>();
                        quickSortInDescendingOrder(weight_array,keys_array, 0, dic.Count - 1);
                        listBox1.DataSource = keys_array.ToList<string>();
                    }

                }         
                else // word isn't in the scenario file >> check whether is correct or not (search it in dictionary). 
                {
                                    
                    listBox1.Visible = false;
                    if (content.Last() == ' ')
                    { 
                        c.Add( c3[num_string]);
                        c.Add("");
                        string[] words = wordslist.ToArray();
                        Dictionary<string , int> editdictance_dic = new Dictionary<string,int> (); 
                        Array.Sort(words);
                        bool flag = Spell_checker(words, 0, words.Length -1, c3[num_string]);
                        if (str2.Length > str.Length) //delete char
                        {
                            if (flag == false) //the value is not in dictionary (wrong spelling)
                            {
                                //label3.Visible = true;
                                for (long i = 0; i <= words.Length - 1; i++)
                                {
                                    int weight = MinNumberOfChanges(c[num_string], words[i], c[num_string].Length, words[i].Length);
                                    editdictance_dic.Add(words[i], weight);
                                }
                                var sorted_editdictance = editdictance_dic.OrderBy(x => x.Value);
                                Dictionary<string, int> sorted_editdistance_dic = sorted_editdictance.ToDictionary(t => t.Key, t => t.Value);
                                string[] words_dic = sorted_editdistance_dic.Keys.ToArray();
                                int[] words_dic_weights = sorted_editdistance_dic.Values.ToArray();
                              

                                long new_indexlower = 0;
                                long new_indexupper = 0;
                                string new_content = " ";
                                for (int i = 0; i <= words_dic.Length - 1; i++)
                                   
                                {
                                    
                                    c[num_string] = words_dic[i];
                                    new_content = string.Join(" ", c);

                                    if (checkBox1.Checked == false)
                                    {
                                        new_indexlower = binsearchlow(sorted_dic_keys, 0, sorted_dic_keys.Length - 1, new_content);
                                        new_indexupper = binsearchupper(sorted_dic_keys, 0, sorted_dic_keys.Length - 1, new_content);
                                        if (new_indexlower != -1)
                                            break;
                                        if (i == words_dic.Length - 1)
                                        {
                                            dic = ndic;
                                            sorted_dic_weights = dic.Values.ToArray<long>();
                                            sorted_dic_keys = dic.Keys.ToArray<string>();
                                            quickSortInAscendingOrder(sorted_dic_weights, sorted_dic_keys, 0, ndic.Count - 1);
                                            i = -1;
                                        }
                                    }
                                    else //correction and substring
                                    {
                                        dic = ndic;
                                        Dictionary<string, long> dic3 = new Dictionary<string, long>();
                                        int counter = 0;
                                        foreach (var key in dic.Keys)
                                        {
                                           
                                            if (key.ToLower().IndexOf(c[num_string].ToLower()) != -1)

                                                if (key.ToLower().IndexOf(c[num_string].ToLower()) != 0)
                                                {
                                                    dic3.Add(key, dic.ElementAt(counter).Value);
                                                    break;
                                                }
                                            counter++;
                                        }
                                        dic = dic3;
                                        
                                    }
                                }
                                c.RemoveAt(c.Count-1);
                                dic = searched_list(new_indexlower, new_indexupper, sorted_dic_keys, sorted_dic_weights);
                                listBox1.Visible = true;
                                string[] s = dic.ElementAt(0).Key.Split(' ');
                                c[num_string] = s[num_string];
                                if (radioButton1.Checked == true)
                                    listBox1.DataSource = insertionSort(dic).ToList<string>();

                                if (radioButton2.Checked == true)
                                {
                                    long[] weight_array = dic.Values.ToArray<long>();
                                    string[] keys_array = dic.Keys.ToArray<string>();
                                    quickSortInDescendingOrder(weight_array, keys_array, 0, dic.Count - 1);
                                    listBox1.DataSource = keys_array.ToList<string>();
                                }
                            }

                            else //search with supstring
                            {
                                string[] c2 = content.Split(' '); // array of strings in content 
                                dic = ndic;
                                Dictionary<string, long> dic2 = new Dictionary<string, long>();
                                int l = 0;
                                foreach (var key in dic.Keys)
                                {
                                    int counter = 0;
                                    int length = c2.Length - 1;
                                    for (int k = 0; k < length; k++)
                                    {
                                        if (key.ToLower().IndexOf(c2[k].ToLower()) != -1)
                                            counter++;
                                        else break;
                                    }
                                    if (counter == length)
                                    {
                                        l++;
                                        dic2.Add(key, dic.ElementAt(l).Value);
                                    }

                                }
                                dic = dic2;
                                if (dic.Count == 0)
                                    label4.Visible = true;
                                else
                                {

                                    listBox1.Visible = true;
                                    if (radioButton1.Checked == true)
                                        listBox1.DataSource = insertionSort(dic).ToList<string>();

                                    if (radioButton2.Checked == true)
                                    {
                                        long[] weight_array = dic.Values.ToArray<long>();
                                        string[] keys_array = dic.Keys.ToArray<string>();
                                        quickSortInDescendingOrder(weight_array, keys_array, 0, dic.Count - 1);
                                        listBox1.DataSource = keys_array.ToList<string>();
                                    }
                                }

                            }
                        }

                    }
               }
          }
            if (!string.IsNullOrWhiteSpace(textBox6.Text.ToString()))
            {
                sorted_dic_weights = dic.Values.ToArray<long>();
                sorted_dic_keys = dic.Keys.ToArray<string>();
            }
          str = textBox6.Text;
 }

  private void label3_Click(object sender, EventArgs e)
  {

  }

  private void label4_Click(object sender, EventArgs e)
  {

  }
}
}
