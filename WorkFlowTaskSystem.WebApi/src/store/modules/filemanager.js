import Cookies from 'js-cookie';
import Util from '@/libs/util'
const filemanager = {
    namespaced:true,
    state: {
        fileUrl:Util.constUrl+"/FileHelper/",
        uploadUrl:Util.constUrl+"/FileHelper/upload",
        downloadUrl:Util.constUrl+"/FileHelper/DownLoad?file=",
        deleteUrl:Util.constUrl+"/FileHelper/DeleteFile?file=",
    },
    mutations: {
        
    },
    actions:{
        
        async check({state},payload){//校验文件
            return await Util.ajaxfile.post('/FileHelper/Check',payload.data);
        },
        async calculate({state},payload){//计算容积率
            return await Util.ajaxfile.post('/FileHelper/Calculate',payload.data);
        },
        async InsertCableConstant({state},payload){
            return await Util.ajax.post('/FileHelper/InsertCableConstant',payload.data);
        },
        async InsertBridgeConstant({state},payload){
            return await Util.ajax.post('/FileHelper/InsertBridgeConstant',payload.data);
        },
        async deleteFile({state},payload){
            await Util.ajax.delete(state.deleteUrl+payload.data);
        },
        
    }
};

export default filemanager;
