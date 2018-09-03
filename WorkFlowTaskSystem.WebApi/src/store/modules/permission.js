import Cookies from 'js-cookie';
import Util from '@/libs/util'
const permission = {
    namespaced:true,
    state: {
        permissions:[],
        children:[],
        permissionLists:[]
    },
    mutations: {
        
    },
    actions:{

        async getAllTree({state},payload){
            
            let rep= await Util.ajax.get('/api/services/app/PermissionInfo/GetAllTree');
            state.permissions=[];
            state.permissions.push(...rep.data.result);
            
            
        },
        async getAll({state},payload){
            
            let rep= await Util.ajax.get('/api/services/app/PermissionInfo/GetAllList');
            state.permissionLists=[];
            state.permissionLists.push(...rep.data.result);
            //Util.ztreeInit(payload.data.obj,payload.data.setting,rep.data.result);
        },
        async getAllPermissionByParentId({state},payload){
            
            
             let query={
                parentId:payload.data.id
            }
            let rep= await Util.ajax.get('/api/services/app/PermissionInfo/GetPermissionByParentId',{params:query});
            state.children=[];
            state.children.push(...rep.data.result);
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/PermissionInfo/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/PermissionInfo/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/PermissionInfo/Update',payload.data);
        }
        
    }
};

export default permission;
