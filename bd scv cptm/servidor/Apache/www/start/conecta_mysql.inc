<?php

	$link=mysql_connect("127.0.0.1","root","");
	
	if(!$link)
		die("Falha de conexуo".mysql_error());
		
	mysql_query("CREATE DATABASE IF NOT EXISTS db_produto",$link) or die ("Falha ao criar o banco de dados!".mysql_error());
       	
	$banco = mysql_select_db("db_produto",$link) or die("Falha na seleчуo do banco".mysql_error());
	
	
	$criatb = "CREATE TABLE IF NOT EXISTS produtos(
					codigo_produto smallint NOT NULL primary key,
					nome_produto varchar(80) NOT NULL,
					descricao_produto text,
					preco float NOT NULL,
					peso float,
					cod_categoria smallint NOT NULL,
					cod_subcategoria smallint NOT NULL,
					adicionais text)"; 
	
	mysql_query ($criatb) or die ("erro na criaчуo da tabela".mysql_error());
	
	

?>