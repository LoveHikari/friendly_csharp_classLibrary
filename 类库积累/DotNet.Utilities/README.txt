DBHelper说明：
数据库连接默认在根目录下Intel\dell.xml下，dell文件格式为<?xml version="1.0" encoding="utf-8"?>
                                                              <SQlConn>
                                                                 <local ConnStr="server=121.41.101.4,5533;uid=kidsnet;pwd=1D#g2!hj3kYt4rwg5r#o6hfd7sr@;database=nynet" providerName="System.Data.SqlClient" />
                                                              </SQlConn>

修改数据库连接位置需添加配置节：<appSettings><add key="dbpath" value="E:\Intel\\dell.xml"/></appSettings>
SQLite需要在app.config文件中注册： <system.data>
                                   <DbProviderFactories>
                                   <remove invariant="System.Data.SQLite"/>
                                   <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".Net Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite" />
                                   </DbProviderFactories>
                                   </system.data>
MySql需要在app.config文件中注册： <system.data>
                                   <DbProviderFactories>
                                   <remove invariant="MySql.Data.MySqlClient"/>
                                   <add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient" description=".Net Framework Data Provider for MySQL" type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data"/>
                                   </DbProviderFactories>
                                   </system.data>
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Excel  ado.net连接：Provider=Microsoft.Ace.OLEDB.12.0;Data Source='1.xlsx';Extended Properties='Excel 12.0;HDR=yes;IMEX=1'  System.Data.OleDb
SQLite ado.net连接：Data Source=data.sdb;Pooling=true;FailIfMissing=false;  System.Data.SQLite
SQL server：server=121.41.101.4,5533;uid=kidsnet;pwd=1D#g2!hj3kYt4rwg5r#o6hfd7sr@;database=nynet  System.Data.SqlClient
Access：Provider=Microsoft.Ace.OLEDB.12.0;Data Source='C:\Users\Administrator\Desktop\Database1.accdb';User Id=; Password=;  System.Data.OleDb
MySql：Database='nzw';Data Source='rdsb0lqub12oujlvf6vb9o.mysql.rds.aliyuncs.com';User Id='zhongfu';Password='zf_liuyan_2015';charset='utf8';pooling=true;  MySql.Data.MySqlClient