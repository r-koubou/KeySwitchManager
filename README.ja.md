[English](README.md)

# キースイッチファイル管理ツール

キースイッチ情報をデータベースで管理し、対応したDAWのキースイッチ定義ファイルに相互変換可能なツール。

![overview](https://i.gyazo.com/db570b52d9c59fad54fc1d7b043a1d21.png)



## 入力フォーマット

- スプレッドシート
- JSON



## 対応DAW

- Cubase (Proのみ)
- Nuendo
- Studio One 5 (Proのみ)



## コマンドラインオプション

### コマンド

共通

```bash
KeySwitchManager.CLI <command> [<options>|--help]
```

- シェルスクリプト

```bash
KeySwitchManager.CLI.sh
```

- DOSバッチ

```bash
KeySwitchManager.CLI.bat
```



## データ作成の流れ

1. キースイッチ情報をデータベースに登録
2. DAWのキースイッチファイルを出力

### キースイッチ情報をデータベースに登録

#### コマンド

- import-xlsx 

スプレッドシートに記入

Template.xlsm を別名コピーして編集後、コマンドラインから

```Bash
KeySwitchManager.CLI import-xlsx -[andpfih]
```


| オプション | 必須 | 説明                 |
| :--------: | ---- | -------------------- |
|     -a     | No   | Author               |
|     -n     | No   | Description          |
|     -d     | Yes  | Developrt name       |
|     -p     | Yes  | Product name         |
|     -f     | Yes  | Database filename    |
|     -i     | Yes  | Spreadsheet filename |



#### コマンド

- template

テンプレート用JSONテキストを出力

- import

JSONファイルを編集後、コマンドラインから

```bash
KeySwitchManager.CLI import -i "jsonfile" -f "Database file"
```



### DAWのキースイッチファイルを出力

#### コマンド

- expressionmap
- studio-one

```bash
KeySwitchManager.CLI <command> -[sdof]
```


|    Command    | 説明                                     |
| :-----------: | ---------------------------------------- |
| expressionmap | Cubase / Nuendo : VST Expressionmap file |
|  studio-one   | Studio One 5 : Keyswitch file            |



| オプション | 必須 | 説明           |
| :--------: | ---- | -------------- |
|     -s     | No   | Author         |
|     -d     | No   | Description    |
|     -o     | Yes  | Developrt name |
|     -f     | Yes  | Product name   |
