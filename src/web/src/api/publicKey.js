import request from '@/utils/request'

export function getPublicKey() {
  return request({
    url: '/security/publicKey',
    method: 'get',
  })
}
