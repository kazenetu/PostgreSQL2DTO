DROP TABLE IF EXISTS m_test;
DROP TABLE IF EXISTS t_test;

-- テストマスタ
CREATE TABLE m_test(
 unique_name varchar(255) NOT NULL,
 password varchar(255) NOT NULL,
 salt varchar(255) NOT NULL,
 disabled boolean default false,
 version integer default 1,
 primary key(unique_name)
);

comment on table m_test              is 'テストマスタ';
comment on column m_test.unique_name is 'ユニークな略称';
comment on column m_test.password    is '暗号化したパスワード';
comment on column m_test.salt        is '暗号化パラメータ';
comment on column m_test.version     is '更新バージョン';

-- テストテーブル
create table t_test (
  col_int integer
  , col_string1 character varying
  , col_string2 character
  , col_decimal numeric
  , col_DateTime1 date
  , col_DateTime2 time
  , col_DateTime3 timestamp
  , col_Bool BOOLEAN
  , col_Float real
  , col_Double double precision
) ;

comment on table t_test is 'テストテーブル:全てのデータ型';
comment on column t_test.col_int is '数値:intになるはず';
comment on column t_test.col_string1 is '文字列:stringになるはず';
comment on column t_test.col_string2 is 'char型:stringになるはず';
comment on column t_test.col_decimal is 'decimal型:decimalになるはず';
comment on column t_test.col_DateTime1 is 'date型:DateTimeになるはず';
comment on column t_test.col_DateTime2 is 'time型:DateTimeOffsetになるはず';
comment on column t_test.col_DateTime3 is 'timestamp:DateTimeOffsetになるはず';
comment on column t_test.col_Bool is 'boolean型:boolになるはず';
comment on column t_test.col_Float is 'float型:floatになるはず';
comment on column t_test.col_Double is 'double型:doubleになるはず';
