DBHelper˵����
���ݿ�����Ĭ���ڸ�Ŀ¼��Intel\dell.xml�£�dell�ļ���ʽΪ<?xml version="1.0" encoding="utf-8"?>
                                                              <SQlConn>
                                                                 <local ConnStr="server=121.41.101.4,5533;uid=kidsnet;pwd=1D#g2!hj3kYt4rwg5r#o6hfd7sr@;database=nynet" providerName="System.Data.SqlClient" />
                                                              </SQlConn>

�޸����ݿ�����λ����������ýڣ�<appSettings><add key="dbpath" value="E:\Intel\\dell.xml"/></appSettings>
SQLite��Ҫ��app.config�ļ���ע�᣺ <system.data>
                                   <DbProviderFactories>
                                   <remove invariant="System.Data.SQLite"/>
                                   <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".Net Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite" />
                                   </DbProviderFactories>
                                   </system.data>
MySql��Ҫ��app.config�ļ���ע�᣺ <system.data>
                                   <DbProviderFactories>
                                   <remove invariant="MySql.Data.MySqlClient"/>
                                   <add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient" description=".Net Framework Data Provider for MySQL" type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data"/>
                                   </DbProviderFactories>
                                   </system.data>
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Excel  ado.net���ӣ�Provider=Microsoft.Ace.OLEDB.12.0;Data Source='1.xlsx';Extended Properties='Excel 12.0;HDR=yes;IMEX=1'  System.Data.OleDb
SQLite ado.net���ӣ�Data Source=data.sdb;Pooling=true;FailIfMissing=false;  System.Data.SQLite
SQL server��server=121.41.101.4,5533;uid=kidsnet;pwd=1D#g2!hj3kYt4rwg5r#o6hfd7sr@;database=nynet  System.Data.SqlClient
Access��Provider=Microsoft.Ace.OLEDB.12.0;Data Source='C:\Users\Administrator\Desktop\Database1.accdb';User Id=; Password=;  System.Data.OleDb
MySql��Database='nzw';Data Source='rdsb0lqub12oujlvf6vb9o.mysql.rds.aliyuncs.com';User Id='zhongfu';Password='zf_liuyan_2015';charset='utf8';pooling=true;  MySql.Data.MySqlClient