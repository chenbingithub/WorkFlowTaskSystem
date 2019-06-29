import Cookies from 'js-cookie';
import Util from '@/libs/util'
const organization = {
    namespaced:true,
    state: {
        organizations:[],
        roles:[],
        pers:[],
    },
    mutations: {
        
    },
    actions:{

        async getAll({state},payload){            
            let rep= await Util.ajax.get('/api/services/app/OrganizationUnit/GetAllTree');
            state.organizations=[];
            state.organizations.push(...rep.data.result);    
        },
        async delete({state},payload){
            await Util.ajax.delete('/api/services/app/OrganizationUnit/Delete?Id='+payload.data.id);
        },
        async create({state},payload){
            await Util.ajax.post('/api/services/app/OrganizationUnit/Create',payload.data);
        },
        async update({state},payload){
            await Util.ajax.put('/api/services/app/OrganizationUnit/Update',payload.data);
        },
        async getorganzition({state},payload){
            let page={
                organzitionId:payload.data
            }
            let rep=await Util.ajax.get('/api/services/app/OrganizationUnit/GetRolePers',{params:page});
            state.roles=[];
            state.roles.push(...rep.data.result.roles);
            state.pers=[];
            state.pers.push(...rep.data.result.permissions);
        }
        
    }
};

export default organization;
