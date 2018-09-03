import Cookies from 'js-cookie';
import Util from '@/libs/util'
const documentFile = {
    namespaced:true,
    state: {
        documentFiles:[],
        uploadurl:Util.constUrl+"/home/upload"
    },
    mutations: {
        setPageSize(state,size){
            state.pageSize=size;
        },
        setCurrentPage(state,page){
            state.currentPage=page;
        }
    },
    actions:{
        async getAll({state},payload){
            let rep= await Util.ajax.get('/api/services/app/DocumentTreeNode/GetNodesByParentId');
            state.documentFiles=[];
            state.documentFiles.push(...rep.data.result);
            state.documentFiles.forEach(function(e,index,arr){
                e.children=[];
            });
        },
        
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/DocumentTreeNode/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/DocumentTreeNode/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/DocumentTreeNode/Update',payload.data);
        },
        async getAllPermissions({state}){
            let rep=await Util.ajax.get('/api/services/app/DocumentTreeNode/GetAllPermissions');
            state.permissions=[];
            state.permissions.push(...rep.data.result.items)

        },
        async getdocumentFile({state},payload){
            let page={
                parentId:payload.data
            }
            let rep=await Util.ajax.get('/api/services/app/DocumentTreeNode/GetNodesByParentId',{params:page});
            var data=rep.data.result;
            data.forEach(function(e,index,arr){
                e.children=[];
            });
            return data;
        }
    }
};

export default documentFile;
