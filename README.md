ConsoleApp22
====

## Overview

ﾀｲﾏｰに設定したInterval毎に処理を実行するIntervalTimerｸﾗｽの検証ﾌﾟﾛｼﾞｪｸﾄ

## Description

IntervalTimerは処理に時間がかかっても設定したInterval毎に処理が実行できます。
例えば1分毎に重い処理を実行したい場合に本ｸﾗｽを採用できます。

通常だと処理中に重い処理を実行するとその分次の処理が遅くなります。
ｲﾍﾞﾝﾄの消化に5秒かかる場合、処理の開始は下記のようになります。

00:00:00
00:01:05
00:02:10
00:03:15
...

IntervalTimerを用いると下記のように1分毎に処理を行えます。

00:00:00
00:01:00
00:02:00
00:03:00

## Demo

## VS. 

## Requirement

.NET Framework 4.6.1

## Usage

## Contribution

## Licence

[MIT](https://github.com/twinbird827/ConsoleApp22/blob/master/LICENSE)

## Author

[twinbird827](https://github.com/twinbird827)