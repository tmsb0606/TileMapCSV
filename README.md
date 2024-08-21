# TileMapCSV
## 概要
Unity内で作成したTileMapをcsvに書き出し、<br>
csvを読み込んでTileMapを生成するエディタ拡張。

## 使い方
## タイルマップ→CSV
ツールバー→MAP→TileMap2CSV <br>
TileMapで生成されたオブジェクトの親オブジェクトをTileMAPに設定。<br>
縦横サイズとoffsetを設定して書き出しボタンを押下。<br>
※csvはプロジェクトファイルのasetts外にSavaDataという名前で生成される。<br>
※タイルマップ作成に使ったprefabをResources/Prefabs/内に保存する必要がある。

## CVS→タイルマップ
ツールバー→MAP→CSV2TileMAP<br>
親オブジェクトとcsvファイルを設定して読み込みボタンを押下。
