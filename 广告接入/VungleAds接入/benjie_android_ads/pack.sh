
FILE_NAME=${PWD##*/}.jar
SRC_FOLDER=${PWD}/bin/classes
DEST_FOLDER=${PWD}/output


pushd ${SRC_FOLDER}

jar -cvf ${FILE_NAME} *

cp ${FILE_NAME} ${DEST_FOLDER}

popd