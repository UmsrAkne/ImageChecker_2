# Image Checker v2

## Overview

ゲームリソースの画像確認用ソフトです。

## Usage

1. 画像本体、または画像ファイルが直下に格納されたディレクトリをウィンドウに対してドラッグアンドドロップします。  
これでファイルをロードすることができます。
2. 読み込まれた画像ファイルはウィンドウ右側のリストに表示され、これを選択するとウィンドウ左側のプレビューにそれが反映されます。
3. 必要に応じて、画像の位置や拡大率を調節します。
4. 特定のショートカットキーで、画像ファイル名を含むテキストをクリップボードに転送します。

以上が基本的な使い方です。

## Grouping

画像をロードする際、画像ファイルの名前が特定の命名規則に従っている場合、画像ファイルは A-D の４つのグループに自動分類されます。

A-D までのアルファベット1文字 + 数字4桁 を含むファイル名が分類対象です。

分類されるファイル名の例は以下です。拡張子は分類判定には無関係です。

    A0000
    B9999
    C0101a
    D01011

## Setting

画像ファイル名を含むテキストをクリップボードにコピーする機能では、`$` に続くアルファベット一文字からなる、以下のキーワードがコピーされる際に置き換えされます。

    $a : A グループに分類されている画像ファイル名
    $b : B グループに分類されている画像ファイル名
    $c : C グループに分類されている画像ファイル名
    $d : D グループに分類されている画像ファイル名
    $x : 画像の X 座標 (スクリーン中央を基準とする座標)
    $y : 画像の Y 座標 (スクリーン中央を基準とする座標)
    $s : 画像の拡大率

ウィンドウのメニューバーの Setting から、上記キーワードを含むテキストを設定できます。

## キー操作

    Ctrl + v : プレビューエリアを表示 / 非表示