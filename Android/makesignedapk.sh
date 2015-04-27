jarsigner -sigalg MD5withRSA -digestalg SHA1 -keystore xample.keystore -signedjar com.ts.yjtt.signed.apk ./bin/Release/com.ts.yjtt.apk publishingdoc
~/Documents/adt/sdk/build-tools/19.1.0/zipalign -v 4 ./com.ts.yjtt.signed.apk ./com.ts.yjtt.signed.align.apk
rm ./com.ts.yjtt.signed.apk
