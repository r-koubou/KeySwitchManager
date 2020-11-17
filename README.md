[Japanese](README.ja.md)

# A Keyswitch file management tool for DAW

This tool manages keyswitch information in a database and can be converted to and from the corresponding DAW keyswitch definition files.

![overview](https://i.gyazo.com/db570b52d9c59fad54fc1d7b043a1d21.png)



## Input Formats

- Spreadsheet
- JSON



## Supported DAWs

- Cubase (Pro only)
- Nuendo
- Studio One 5 (Pro only)



## Command Line Options

### Command

Common

```bash
KeySwitchManager.CLI <command> [<options>|--help]
```

- Shell Script

```bash
KeySwitchManager.CLI.sh
```

- DOS Batch

```bash
KeySwitchManager.CLI.bat
```



## The flow of data creation

1. Register the keyswitch information to the database
2. Output the DAW keyswitch file

### Register the keyswitch information to the database

#### Command

- import-xlsx 

Fill out the spreadsheet

Copy Template.xlsm under an alias and run the following command after editing it.

```Bash
KeySwitchManager.CLI import-xlsx -[andpfih]
```


| Option | Required | Description          |
| :----: | -------- | -------------------- |
|   -a   | No       | Author               |
|   -n   | No       | Description          |
|   -d   | Yes      | Developrt name       |
|   -p   | Yes      | Product name         |
|   -f   | Yes      | Database filename    |
|   -i   | Yes      | Spreadsheet filename |



#### Command

- template

Output JSON text for templates

- import

After editing the JSON file, run the following command.

```bash
KeySwitchManager.CLI import -i "jsonfile" -f "Database file"
```



### Output the DAW keyswitch file

#### Command

- expressionmap
- studio-one

```bash
KeySwitchManager.CLI <command> -[sdof]
```


|    Command    | Description                              |
| :-----------: | ---------------------------------------- |
| expressionmap | Cubase / Nuendo : VST Expressionmap file |
|  studio-one   | Studio One 5 : Keyswitch file            |



| Option | Required | Description    |
| :----: | -------- | -------------- |
|   -s   | No       | Author         |
|   -d   | No       | Description    |
|   -o   | Yes      | Developrt name |
|   -f   | Yes      | Product name   |
