using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ConfigFile : MonoBehaviour {

    public string ReadKey(string filename, string section, string key)
    {
        string line; // each line read
        StreamReader r = new StreamReader(filename); // ini.txt
        int rpeek; // checks if next line is a potential key or not
        string lineKey; // reads key name

        // Error variables
        bool SectionExists = false;

        using (r)
        {
            while (!r.EndOfStream) // repeat reading line until done
            {
                line = r.ReadLine(); // read that fuckin line
                if(line != null) // make sure that line isnt empty
                {
                    if (line == "[" + section + "]") // if that line a section?
                    {
                        rpeek = r.Peek();
                        SectionExists = true; // tells that the section does exist
                        while(rpeek != '[' && rpeek != ' ' && rpeek != -1 && rpeek != 13) // checks to see if the next line could be a potential key
                        {                          
                            line = r.ReadLine();
                            lineKey = line.Substring(0, line.IndexOf('=')); // get key name
                            if (lineKey == key) // check key name
                            {
                                string result = line.Substring(line.IndexOf('=') + 1);
                                r.Close();
                                return result;
                            }
                            rpeek = r.Peek(); // repeat peeks until empty
                            Debug.Log("Reads '" + rpeek + "'");
                        }
                    }
                }
            }
            r.Close();
        }

        if (!SectionExists) Debug.Log("Error reading key from ini file: '[" + section + "]' does not exist.");
        else Debug.Log("Error reading key from ini file: '[" + section + "] " + key + "', does not exist.");

        return "";
    }
    /*
    public int ReadKeyInt(string filename, string section, string key)
    {
        string line; // each line read
        StreamReader r = new StreamReader(filename); // ini.txt
        int rpeek; // checks if next line is a potential key or not
        string lineKey; // reads key name

        // Error variables
        bool SectionExists = false;

        using (r)
        {
            while (!r.EndOfStream) // repeat reading line until done
            {
                line = r.ReadLine(); // read that fuckin line
                if (line != null) // make sure that line isnt empty
                {
                    if (line == "[" + section + "]") // if that line a section?
                    {
                        rpeek = r.Peek();
                        SectionExists = true; // tells that the section does exist
                        while (rpeek != '[' && rpeek != ' ' && rpeek != -1 && rpeek != 13) // checks to see if the next line could be a potential key
                        {
                            line = r.ReadLine();
                            lineKey = line.Substring(0, line.IndexOf('=')); // get key name
                            if (lineKey == key) // check key name
                            {
                                string resultString = line.Substring(line.IndexOf('=') + 1);
                                int result;
                                bool isNumber = int.TryParse(resultString, out result); // checks if result is an int

                                if (!isNumber)
                                {
                                    // if fails
                                    Debug.Log("Error reading key from ini file: '[" + section + "] " + key + "=" + resultString + "', is not an integer.");
                                    r.Close();
                                    return -1;
                                }
                                else
                                {
                                    // if succeeds
                                    r.Close();
                                    return result;
                                }
                            }
                            rpeek = r.Peek(); // repeat peeks until empty
                        }
                    }
                }
            }
            r.Close();
        }

        if (!SectionExists) Debug.Log("Error reading key from ini file: '[" + section + "]' does not exist.");
        else Debug.Log("Error reading key from ini file: '[" + section + "] " + key + "', does not exist.");

        return -1;
    }
    */
    public void WriteSection(string filename, string section)
    {
        List<string>lineFile = new List<string>(); ;
        string line;
        int i = 0;
        StreamReader r = new StreamReader(filename);
        using (r) // copys all strings in the file into a vector
        {
            while (!r.EndOfStream)
            {
                line = r.ReadLine();
                lineFile.Add(line);
                i++;
            }
            r.Close();
        }

        for (i = 0; i < lineFile.Count; i++) // checks if the section already exists
        {
            if (lineFile[i] == "[" + section + "]")
            {
                return;
            }
            
        }

        // adds section
        lineFile.Add("");
        lineFile.Add("[" + section + "]");        

        File.WriteAllLines(filename, lineFile.ToArray());
        return;
    }

    public void WriteKey(string filename, string section, string key, string value)
    {
        List<string> lineFile = new List<string>(); ;
        string line;
        int i = 0;
        StreamReader r = new StreamReader(filename);
        using (r) // copys all strings in the file into a vector
        {
            while (!r.EndOfStream)
            {
                line = r.ReadLine();
                lineFile.Add(line);
                i++;
            }
            r.Close();
        }

        // Checks to see if key exists, if not, it creates the key
        bool SectionExists = false;
        bool KeyExists = false;
        string firstChar;

        for (i = 0; i < lineFile.Count; i++)
        {
            if (lineFile[i] == "[" + section + "]") // checks for the correct section
            {
                SectionExists = true;
                i++;
                if (i >= lineFile.Count) break; // assures that i doesnt go out of range

                firstChar = lineFile[i].Substring(0, 1);
                while (i < lineFile.Count && firstChar != "[" && firstChar != "" && firstChar != " ") // checks is next line isnt a section or empty
                {
                    if (lineFile[i].Substring(0, lineFile[i].IndexOf('=')) == key) // checks if the key already exists
                    {
                        lineFile[i] = key + "=" + value;
                        KeyExists = true;
                        break;
                    }
                    i++;
                    if (i >= lineFile.Count) break; // assures that i doesnt go out of range

                    firstChar = lineFile[i].Substring(0, 1);
                }

                if (!KeyExists) // if key didnt exist, create a new one
                {
                    lineFile.Insert(i, key + "=" + value);
                    break;
                }
            }

        }

        if (!SectionExists) // if section didnt exist, create section and then key
        {
            lineFile.Add("");
            lineFile.Add("[" + section + "]");
            lineFile.Add(key + "=" + value);
        }

        File.WriteAllLines(filename, lineFile.ToArray());
    }

}
