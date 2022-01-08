# PostgreSQL2DTO
PostgreSQLのテーブルから.NET用のDTOを作る

## はじめに
PostgreSQLのテーブル情報を取得し、C#のDTOクラスを作成するプログラムです。

## 実行環境
* .NET 5  

## 実行方法
* ローカル実行  
    dotnet runで実行する。  
    ```sh
    dotnet run --project ./console/console.csproj [ファイル出力先] [DBサーバー(サーバ名やIPアドレス))] [ユーザーID] [パスワード] [データベース名] [ポート番号(省略可)]
    ```  
