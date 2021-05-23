# KeySwitchManager

キースイッチ情報をデータベースファイルで管理し、DAWのキースイッチ定義ファイルに変換可能なコマンドラインツール。



## コマンド

```bash
KeySwitchManager.CLI <command> [<options>|--help]
```

コマンドラインオプションに何も指定しない場合は、使用方法を出力する。



## 対応DAW

- Cubase (Proのみ), Nuendo
- Studio One 5.2
- Cakewalk



## データ作成

### キースイッチ情報作成用のファイルの作成、編集

```bash
KeySwitchManager.CLI template
```


キースイッチ情報をデータベースファイルに登録・更新
DAWのキースイッチファイルを出力

### 

## 

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
