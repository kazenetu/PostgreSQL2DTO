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
    dotnet run --project ./src/Presentation/ConsoleApp/ConsoleApp.csproj [NameSpace] [ファイル出力先] [DBサーバー(サーバ名やIPアドレス))] [ユーザーID] [パスワード] [データベース名] [ポート番号(省略可)]
    ```  
    ※具体的な内容は「Dockerコンテナでの実行」を参照

* Dockerコンテナでの実行  
    Dockerコンテナ上で開発環境を構築する。  
   * 前提  
     * Docker EngineやDocker Desktopがインストール済みであること。

   * 実行手順  
     PostgreSQLコンテナとdotnetコンテナを起動する。
      1. docker_devに移動  
          ```sh
          cd docker_dev
          ```

      1. (**初回のみ**)ビルド  
          ```sh
          docker-compose build
          ```

      1. コンテナ起動  
          ```sh
          docker-compose up -d
          ```

      1. コンテナに入る  
          ```sh
          docker exec -it docker_dev_dotnet_1 /bin/bash
          ```

      1. コンテナ内で実行 
          1. dotnet runで実行する。
              ```sh
              dotnet run --project ./src/Presentation/ConsoleApp/ConsoleApp.csproj DB.Dto CSOutputs postgresql_server test test testDB
              ```

          1. コンテナから離脱する。
              ```sh
              exit
              ```

      1. コンテナ停止・削除  
          ```sh
          docker-compose down
          ```

## テスト
### PostgreSQLの利用について
DBリポジトリのテスト「TestDBRepository」ではPostgreSQLを利用する。  
テストする場合は下記のいずれかを行う。
* Dockerコンテナを利用する。  
  ※後述

* 既存のPostgreSQLにテスト用テーブルを作成、指定する。  
  1. DBに「src\PostgreSQL2DTOTest\Config\SQL\init.sql」を実行し、テストテーブルを作成する。
  
  1. 「src\PostgreSQL2DTOTest\Config\db.json」にDBの接続情報を設定する。

* DBリポジトリのテストを実施しない。  
  テスト除外指定を行う。  
  ```sh
  dotnet test ./src/PostgreSQL2DTOTest/PostgreSQL2DTOTest.csproj --filter Category!=InfrastructureTest
  ```  

### テスト方法
* ローカル実行  
   * すべて実行する場合  
        dotnet runで実行する。  
        ```sh
        dotnet test ./src/PostgreSQL2DTOTest/PostgreSQL2DTOTest.csproj
        ```  

   * DBアクセスなどインフラ層のテストを除外する場合  
        dotnet runで実行する。  
        ```sh
        dotnet test ./src/PostgreSQL2DTOTest/PostgreSQL2DTOTest.csproj --filter Category!=InfrastructureTest
        ```  

* Dockerコンテナでの実行  
    Dockerコンテナ上で開発環境を構築する。  
   * 前提  
     * Docker EngineやDocker Desktopがインストール済みであること。

   * 実行手順  
     PostgreSQLコンテナとdotnetコンテナを起動する。
      1. docker_devに移動  
          ```sh
          cd docker_dev
          ```

      1. (**初回のみ**)ビルド  
          ```sh
          docker-compose build
          ```

      1. コンテナ起動  
          ```sh
          docker-compose up -d
          ```

      1. コンテナに入る  
          ```sh
          docker exec -it docker_dev_dotnet_1 /bin/bash
          ```

      1. コンテナ内で実行 
          1. テストを実行する。  
              ```sh
              dotnet test ./src/PostgreSQL2DTOTest/PostgreSQL2DTOTest.csproj
              ```

          1. コンテナから離脱する。  
              ```sh
              exit
              ```

      1. コンテナ停止・削除  
          ```sh
          docker-compose down
          ```
